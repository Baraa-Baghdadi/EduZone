import { ToasterService } from '@abp/ng.theme.shared';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CategoryService } from '@proxy/categories';
import { CourseService, UpdateCourseInput } from '@proxy/courses';
import { LessonDtoForAddCourse, LessonService, UpdateLessonInput } from '@proxy/lessons';
import { finalize, tap } from 'rxjs';
import { getStorage, ref, uploadBytesResumable, getDownloadURL,deleteObject,listAll   } from "firebase/storage";
import { animate, state, style, transition, trigger } from '@angular/animations';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-create-course',
  templateUrl: './create-course.component.html',
  styleUrl: './create-course.component.scss',
  animations:[trigger('fade',[ 
    state('void',style({opacity:0})), 
    transition(':enter',[ animate(500) ]) 
  ]) 
]
})
export class CreateCourseComponent implements OnInit {
  active = 1;
  // For add new course and update:
  allCategories;
  isModalOpen =false;
  isModalOpenForShowImage = false;
  form:FormGroup;
  form2 : FormGroup;
  updateLessonForm:FormGroup;
  readonly imageMaxSize = 51200000;
  readonly videoMaxSize = 512000000000000;
  imageError : string;
  selected?: any;
  // lesson:
  allLessons : LessonDtoForAddCourse[] = [];
  videoDuration: string | null = null;
  videoUrl : string = null;
  videoName : string = null;
  disableAddNewLesson = false;
  // errors:
  addNewLessError = null;
  updateLessonError = null;

  selectedLesson;
  @ViewChild('videoPlayer') videoPlayer!: ElementRef<HTMLVideoElement>;
  constructor(
    private service : CourseService,private fb:FormBuilder,
    private categoryServ:CategoryService,
    private toaster : ToasterService,private router : Router,
    private activatedRout : ActivatedRoute,
    private lessonService : LessonService ){
    }

  ngOnInit(): void {
    this.getAllCategory();
    this.buildForm();
    this.buildForm2();
    this.getDataFromUrl();
  }

  getDataFromUrl(){
    this.activatedRout.params.subscribe((data)=>{
      if (data.id && data.id!= undefined) {
        this.getCourseToEdit(data.id);
      }
      else{
        this.buildForm();
        this.buildForm2();
      }
    })
  }
  
  updateLesson(title,order){
    this.isModalOpen = true;    
    this.selectedLesson = this.allLessons.filter(x => x.title == title && x.videoOrder == order);
    this.buildUpdateLessonForm();  
  }

  buildForm(){
    const {
      title,
      description,
      price,
      newPrice,
      blop,
      fileType,
      fileName,
      fileSize,
      isIconUpdated,
      categoryId,
      lessons
    } = this.selected || {};

    this.form = this.fb.group({
      title : [title ?? '' ,Validators.required],
      description : [description ?? null ,Validators.required],
      price : [price ?? null, Validators.required],
      newPrice : [newPrice ?? null, Validators.required],
      blop : [blop ?? null , Validators.required],
      fileType : [fileType ?? 'jpeg',Validators.required],
      fileName : [fileName ?? "" ,Validators.required],
      fileSize : [fileSize ?? 0],
      isIconUpdated :[isIconUpdated ?? false],
      categoryId : [categoryId ?? null ,Validators.required],
      lessons : [lessons ?? null],
    });
  }


  buildForm2(){
    this.form2 = this.fb.group({
      name : [null],
      lessonTitle : [null,Validators.required],
      lessonContent: [null,Validators.required],
      duration:[null],
      fileSize:[null],
      videoOrder: [null,Validators.required],
      url:[null],
    });
  }

  buildUpdateLessonForm(){
    this.updateLessonForm = this.fb.group({
      id:[this.selectedLesson[0].id ],
      title : [this.selectedLesson[0].title ?? null,Validators.required],
      content: [this.selectedLesson[0].content ?? null,Validators.required],
      videoOrder: [this.selectedLesson[0].videoOrder ?? null,Validators.required],
    });
  }

  save(){
    if (this.form.invalid && this.allLessons.length === 0 ) return;
    const request = this.selected
    ? this.updateCourse()    //this.service.update(this.selected.id,this.form.value) 
    : this.SaveCourse();
  }


  fileChangeListener(fileInput:any){
    console.log(fileInput);  
    //check if a file has been added
    if (fileInput.target.files && fileInput.target.files[0]) {
      let file = fileInput.target.files[0];
      let fileType = file.type;
      if (fileType !== "image/png" && fileType !== "image/jpeg" && fileType !== "image/jpg") {
        this.imageError = 'UploadImageAcceptedTypesError';
        this.fileType.setValue('jpeg');
        this.blop.setValue('null');
        this.fileSize.setValue(0);
        this.fileName.setValue('');
        return false;
      }
      if (file.size > this.imageMaxSize) {
        this.fileType.setValue('jpeg');
        this.blop.setValue('null');
        this.fileSize.setValue(0);
        this.fileName.setValue('');
        return false;
      }
      const attachmentType = file.name.toLowerCase().substring(file.name.toLowerCase().lastIndexOf('.') + 1);
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.fileType.setValue(attachmentType);
        this.blop.setValue(reader.result.toString().replace('data:','').replace(/^.+,/,''));
        this.fileSize.setValue(file.size);
        this.fileName.setValue(file.name.toLowerCase());
        this.isIconUpdated.setValue(true);
      }
    }
  }

  getCourseToEdit(id:any){
    this.service.getCourseByIdById(id).subscribe((data:any) => {
      console.log(data);
      this.selected = data;
      this.allLessons = data.lessons;
      this.lessons.setValue(data.lessons);
      this.buildForm();
      this.buildForm2();
      this.makeCourseImageUnRequired();
      console.log(this.allLessons);
    });
  }



  get blop(){
    return this.form.get('blop') as FormControl;
  }
  get fileType(){
    return this.form.get('fileType') as FormControl;
  }
  get fileName(){
    return this.form.get('fileName') as FormControl;
  }
  get fileSize(){
    return this.form.get('fileSize') as FormControl;
  }
  get lessons(){
    return this.form.get('lessons') as FormControl;
  }
  get isIconUpdated(){
    return this.form.get('isIconUpdated') as FormControl;
  }
  get price(){
    return this.form.get('price') as FormControl;
  }

  getAllCategory(){
    this.categoryServ.getCategoriesForDrop().subscribe(data => {
      this.allCategories = data;
    })
  }


  fileChangeListenerForVideo(fileInput:any){   
    this.selectedVideo = null;
    //check if a file has been added
    if (fileInput.target.files && fileInput.target.files[0]) {
      let file = fileInput.target.files[0];
      this.selectedVideo =  fileInput.target.files[0];
      let fileType = file.type;
      this.videoSize.setValue(file.size * 0.000001);          
      if (fileType !== "video/mp4" && fileType !== "video/avi" && fileType !== "video/mov") {
        this.imageError = 'UploadImageAcceptedTypesError';
        return false;
      }
      if (file.size > this.videoMaxSize) {
        return false;
      }    
      const url = URL.createObjectURL(file);
      this.videoUrl = url;   
      this.videoName = file.name;    
      this.disableAddNewLesson = false;   
    }
  }

  getVideoDuration(video: HTMLVideoElement) {
    this.videoDuration  = this.formatDuration(video.duration); // duration in seconds
  }

  formatDuration(seconds: number): string {
    const minutes = Math.floor(seconds / 60);
    const secs = Math.floor(seconds % 60);

    // Pad with leading zeros if necessary
    const formattedMinutes = String(minutes).padStart(2, '0');
    const formattedSeconds = String(secs).padStart(2, '0');
    return `${formattedMinutes}:${formattedSeconds}`;
  }

  addNewLesson(){
    this.disableAddNewLesson = true;
    var lesson = {} as LessonDtoForAddCourse;
    lesson.title = this.lessonTitle.value;
    lesson.content = this.lessonContent.value;
    lesson.videoOrder = this.videoOrder.value;
    lesson.duration = this.videoDuration;
    lesson.name = this.videoName;
    lesson.fileSize = this.videoSize.value;
    if (this.allLessons.length != 0 ) {
      this.addNewLessError = null;
      this.allLessons.forEach(element => {
        if (element.videoOrder == lesson.videoOrder) {
          this.addNewLessError = "::oAlreadyExist";
          this.toaster.error(this.addNewLessError,"Error");
          return;
        }
        else if(element.title == lesson.title){
          this.addNewLessError = "::vAlreadyExist";
          this.toaster.error(this.addNewLessError,"Error");
          return;
        }
      });
      if (this.addNewLessError === null) {
        this.uploadFile(lesson);
        this.toaster.info("::uploadingNow");
        this.addNewLessError = null;
      }
    }
    else{
      this.uploadFile(lesson);
      this.toaster.info("::uploadingNow");       
    }
  }
  
  clear(){
    this.form2.reset();
    this.selectedVideo = null;
    this.videoDuration = null;
    this.videoUrl  = null;
    this.videoName  = null;
  }

  resetLessonVideo(){
    this.selectedVideo = null;
    this.videoDuration = null;
    this.videoUrl  = null;
    this.videoName  = null;
  }

  removeFromLessons(videoOrder,videoName){
    this.allLessons = this.allLessons.filter(x => x.videoOrder != videoOrder);
    this.deleteFileFromFirebase(videoName);
  }

  get name(){
    return this.form2.get('name') as FormControl;
  }
  get lessonTitle(){
    return this.form2.get('lessonTitle') as FormControl;
  }
  get lessonContent(){
    return this.form2.get('lessonContent') as FormControl;
  }
  get videoOrder(){
    return this.form2.get('videoOrder') as FormControl;
  }
  get url(){
    return this.form2.get('url') as FormControl;
  }
  get videoSize(){
    return this.form2.get('fileSize') as FormControl;
  }
  get duration(){
    return this.form2.get('duration') as FormControl;
  }


  // Upload File in Firebase:
  selectedVideo: File | null = null;
  downloadURL: any = null;
  filePath : string = "";

  storage = getStorage();

  progress = 0 ;
  uploadFile(lesson) {
    if (this.selectedVideo) {
      this.filePath = `uploads/${this.selectedVideo.name}`;
      // Upload file and metadata to the object 'images/mountains.jpg'
      var storageRef = ref(this.storage, this.filePath);
      // with meta data:
      // var uploadTask = uploadBytesResumable(storageRef, this.selectedFile, this.metadata);
      var uploadTask = uploadBytesResumable(storageRef, this.selectedVideo);
      // Listen for state changes, errors, and completion of the upload.
      uploadTask.on('state_changed',
        (snapshot) => {
          // Get task progress, including the number of bytes uploaded and the total number of bytes to be uploaded
          this.progress = Math.round((snapshot.bytesTransferred / snapshot.totalBytes) * 100);
          switch (snapshot.state) {
            case 'paused':
              console.log('Upload is paused');
              break;
            case 'running':
              break;
          }
        },
        (error) => {
          // A full list of error codes is available at
          // https://firebase.google.com/docs/storage/web/handle-errors
          switch (error.code) {
            case 'storage/unauthorized':
              // User doesn't have permission to access the object
              break;
            case 'storage/canceled':
              // User canceled the upload
              break;

            // ...

            case 'storage/unknown':
              // Unknown error occurred, inspect error.serverResponse
              break;
          }
        },
        () => {
          // Upload completed successfully, now we can get the download URL
          getDownloadURL(uploadTask.snapshot.ref).then((downloadURL) => {
            this.downloadURL = downloadURL ;
            this.url.setValue(this.downloadURL);
            lesson.url = downloadURL;
            this.allLessons.push(lesson);
            this.allLessons = [...this.allLessons]; 
            this.clear();
            this.form2.reset();
            this.toaster.info("::successfulyUploaded");
            this.progress = 0 ;  
            this.disableAddNewLesson = false;
            console.log("all Lessons",this.allLessons);
          });
        }
      );
    }
  }

  deleteFileFromFirebase(fileName: string){
    // Create a reference to the file to delete
    const desertRef = ref(this.storage, `uploads/${fileName}`);
    // Delete the file
    deleteObject(desertRef).then(() => {
      // File deleted successfully:
      this.toaster.info("::removalSuccessful");      
    }).catch((error) => {
      // Uh-oh, an error occurred!
    });
  }

  SaveCourse(){
    this.lessons.setValue(this.allLessons);
    this.service.createNewCourseByInput(this.form.value).subscribe(data => {
      this.toaster.info("::successfulyUploaded");
      this.router.navigate(["/my-courses"]);
    });    
  }

  updateCourse(){
    this.lessons.setValue(this.allLessons);
    var updateCourseInput = {id:this.selected.id,...this.form.value} as UpdateCourseInput;
    console.log("updatedCourse",updateCourseInput); 
    this.service.updateCourse(updateCourseInput).subscribe(data => {
      this.toaster.info("::successfulyUpdated");
      this.router.navigate(["/my-courses"]);
    }
    )
  }

  makeCourseImageUnRequired(){
    this.blop.setValidators(null);
    this.blop.updateValueAndValidity();
    this.fileName.setValidators(null);
    this.fileName.updateValueAndValidity();
    this.fileSize.setValidators(null);
    this.fileSize.updateValueAndValidity();
    this.fileType.setValidators(null);
    this.fileType.updateValueAndValidity();
  }

  get updateLessonId(){
    return this.updateLessonForm.get('id') as FormControl;
  }
  get updateLessonTitle(){
    return this.updateLessonForm.get('title') as FormControl;
  }
  get updateLessonContent(){
    return this.updateLessonForm.get('content') as FormControl;
  }
  get updateLessonVideoOrder(){
    return this.updateLessonForm.get('videoOrder') as FormControl;
  }


  updateLeason(){
    this.updateLessonError = null;
    this.allLessons.forEach(element => {
      if (element.id != this.updateLessonId.value  && element.videoOrder == this.updateLessonVideoOrder.value) {
        this.updateLessonError = "::oAlreadyExist";
        this.toaster.error(this.updateLessonError,"Error");
        return;
      }
      else if(element.id != this.updateLessonId.value && element.title == this.updateLessonTitle.value){
        this.updateLessonError = "::vAlreadyExist";
        this.toaster.error(this.updateLessonError,"Error");
        return;
      }
    });
    if (this.updateLessonError === null) {
      var payLoad = {} as UpdateLessonInput;
      payLoad.id = this.updateLessonId.value;
      payLoad.title = this.updateLessonTitle.value;
      payLoad.content = this.updateLessonContent.value;
      payLoad.videoOrder = this.updateLessonVideoOrder.value;
      this.lessonService.updateLessonByInput(payLoad).subscribe((data)=>{
        this.toaster.info("::successfullyUpdated");
        this.isModalOpen = false ;
        this.getDataFromUrl();
        this.updateLessonError = null ;
      });
    }
  }

 
}
