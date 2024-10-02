import { PagedResultDto } from '@abp/ng.core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CategoryService } from '@proxy/categories';
import { CourseDto, CourseService, GetCoursesInput } from '@proxy/courses';
import { debounceTime, distinctUntilChanged, finalize, fromEvent, map, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-my-courses',
  templateUrl: './my-courses.component.html',
  styleUrl: './my-courses.component.scss',
  animations:[trigger('fade',[ 
    state('void',style({opacity:0})), 
    transition(':enter',[ animate(500) ]) 
  ]) 
]
})
export class MyCoursesComponent {
  @ViewChild('search') search !: ElementRef ;
  title = 'type a head search';
  coursesFilter = {} as GetCoursesInput;
  courses = { items: [], totalCount: 0 } as PagedResultDto<CourseDto>;
  state = false ;


  constructor(private service : CourseService){}

  ngOnInit() {
    this.getAllCourses();
  }

  getAllCourses(){
    this.service.getMyCoursesByInput(this.coursesFilter).subscribe({
      next : (data:any) => {this.courses = data;console.log(data);
      }
    });

  }

  ngAfterViewInit(): void {
    fromEvent(this.search.nativeElement,'keyup').pipe(
      debounceTime(100),
      map((evt:any)=>evt.target.value),
      tap(()=>this.state = true) ,
      distinctUntilChanged(),
      switchMap((value:string)=>{
        if (value.length == 0) {
         this.state =false ;
         this.courses = { items: [], totalCount: 0 };
         this.getAllCourses();
         return [];
        }
        return this.service.getMyCoursesByInput(this.coursesFilter);
      }),
      tap(()=>this.state =false)
    ).subscribe((data:any)=> this.courses = data)
  }

  showDetailsOfCourse(rowId){
  }

  // For add new course and update:
  // allCategories;
  // isModalOpen =false;
  // isModalOpenForShowImage = false;
  // form:FormGroup;
  // readonly imageMaxSize = 512000;
  // imageError : string;
  // selected?: any;
  

  // openCourseModal(id:string = null){
  //   if (id) {
  //     this.getCourseToEdit(id);
  //   }
  //   else{
  //     this.selected = null;
  //     this.showForm();
  //   }
  // }

  // showForm(){
  //   this.buildForm();
  //   this.isModalOpen = true;
  // }

  // buildForm(){
  //   const {
  //     title,
  //     description,
  //     price,
  //     newPrice,
  //     blop,
  //     fileType,
  //     fileName,
  //     fileSize,
  //     isIconUpdated,
  //     categoryId,
  //     lessons
  //   } = this.selected || {};

  //   this.form = this.fb.group({
  //     title : [title ?? '' ,Validators.required],
  //     description : [description ?? null ,Validators.required],
  //     price : [price ?? null, Validators.required],
  //     newPrice : [newPrice ?? null, Validators.required],
  //     blop : [blop ?? null , Validators.required],
  //     fileType : [fileType ?? 'jpeg',Validators.required],
  //     fileName : [fileName ?? "" ,Validators.required],
  //     fileSize : [fileSize ?? 0],
  //     isIconUpdated :[isIconUpdated ?? false],
  //     categoryId : [categoryId ?? null ,Validators.required],
  //     lessons : [lessons ?? [] ,Validators.required],
  //   });
  // }

  // save(){
  //   console.log(this.form.value);
  //   if (this.form.invalid || this.blop.valid === null || this.blop.value === undefined ) return;
  //   const request = this.selected
  //   ? this.service.createNewCourseByInput(this.form.value)     //this.service.update(this.selected.id,this.form.value) 
  //   : this.service.createNewCourseByInput(this.form.value);

  //   request
  //   .pipe(
  //     finalize(() => this.isModalOpen = false),
  //     tap(() => {this.form.reset();}),
  //   ).subscribe(data => this.getAllCourses()) 
  // }


  // fileChangeListener(fileInput:any){
  //   console.log(fileInput);  
  //   //check if a file has been added
  //   if (fileInput.target.files && fileInput.target.files[0]) {
  //     let file = fileInput.target.files[0];
  //     let fileType = file.type;
  //     if (fileType !== "image/png" && fileType !== "image/jpeg" && fileType !== "image/jpg") {
  //       this.imageError = 'UploadImageAcceptedTypesError';
  //       this.fileType.setValue('jpeg');
  //       this.blop.setValue('null');
  //       this.fileSize.setValue(0);
  //       this.fileName.setValue('');
  //       return false;
  //     }
  //     if (file.size > this.imageMaxSize) {
  //       this.fileType.setValue('jpeg');
  //       this.blop.setValue('null');
  //       this.fileSize.setValue(0);
  //       this.fileName.setValue('');
  //       return false;
  //     }
  //     const attachmentType = file.name.toLowerCase().substring(file.name.toLowerCase().lastIndexOf('.') + 1);
  //     const reader = new FileReader();
  //     reader.readAsDataURL(file);
  //     reader.onload = () => {
  //       this.fileType.setValue(attachmentType);
  //       this.blop.setValue(reader.result.toString().replace('data:','').replace(/^.+,/,''));
  //       this.fileSize.setValue(file.size);
  //       this.fileName.setValue(file.name.toLowerCase());
  //       this.isIconUpdated.setValue(true);
  //     }
  //   }
  // }

  // getCourseToEdit(id:any){
  //   this.service.getCourseByIdById(id).subscribe((data:any) => {
  //     this.selected = data;
  //     this.buildForm();
  //     this.isModalOpen = true;
  //   });
  // }



  // get blop(){
  //   return this.form.get('blop') as FormControl;
  // }
  // get fileType(){
  //   return this.form.get('fileType') as FormControl;
  // }
  // get fileName(){
  //   return this.form.get('fileName') as FormControl;
  // }
  // get fileSize(){
  //   return this.form.get('fileSize') as FormControl;
  // }
  // get isIconUpdated(){
  //   return this.form.get('isIconUpdated') as FormControl;
  // }

  // getAllCategory(){
  //   this.categoryServ.getCategoriesForDrop().subscribe(data => {
  //     this.allCategories = data;
  //   })
  // }
}
