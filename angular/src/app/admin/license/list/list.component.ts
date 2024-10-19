import { PagedResultDto } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { GetLicenseInput, LicenseDto, LicenseService } from '@proxy/licenses';
import { debounceTime, distinctUntilChanged, finalize, fromEvent, map, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrl: './list.component.scss'
})
export class ListComponent implements OnInit {
  @ViewChild('search') search !: ElementRef ;
  title = 'type a head search';
  licenseFilter = {} as GetLicenseInput;
  licenses = { items: [], totalCount: 0 } as PagedResultDto<LicenseDto>;
  state = false ;

  // for create or update license:
  isModalOpen = false;
  form:FormGroup;
  selected?: any;

  constructor(private service : LicenseService,private fb:FormBuilder,
    private toaster : ToasterService
  ){}

  ngOnInit() {
    this.getAllLicenses();
  }

  getAllLicenses(){
    this.service.getList(this.licenseFilter).subscribe({
      next : (data:any) => {this.licenses = data;console.log(data);
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
         this.licenses = { items: [], totalCount: 0 };
         this.getAllLicenses();
         return [];
        }
        return this.service.getList(this.licenseFilter);
      }),
      tap(()=>this.state =false)
    ).subscribe((data:any)=> this.licenses = data)
  }

  openModal(licenseId = null){  
    if (licenseId !== null) {
      this.getLicenseToEdit(licenseId);
      this.buildForm();
      this.isModalOpen = true;
    }
    else{
      this.isModalOpen = true;
      this.buildForm();
    }
  }

  showDetailsOfLicense(rowId){
  }


  buildForm(){
    const {
      key,
      expirationDate
    } = this.selected || {};

    this.form = this.fb.group({
      key : [key ?? '' ,Validators.required],
      expirationDate : [expirationDate ?? this.formatExpirationDateToShowing(new Date()) ,Validators.required]
    });
  }

  get key(){
    return this.form.get('key') as FormControl;
  }
  get expirationDate(){
    return this.form.get('expirationDate') as FormControl;
  }

  getLicenseToEdit(id){
    this.service.getByIdById(id).subscribe((data:any) => {
      this.selected = data;     
      this.buildForm();
      var date = this.formatExpirationDateToShowing(this.selected.expirationDate);
      this.expirationDate.setValue(date);  
      this.key.disable();
    });
  }

  resetForm(){
    this.selected = null;
    this.form.reset();
  }

  save(){
    if (this.form.invalid) return;
    if (this.selected != null) {
      this.expirationDate.setValue(new Date(this.expirationDate.value));
      this.key.enable();
    }

    const request = this.selected
    ? this.service.updateLicanseByIdAndInput(this.selected.id,this.form.value) // update
    : this.service.createLicanseByInput(this.form.value); // create

    request
    .pipe(
      finalize(() => this.isModalOpen = false),
      tap(() => {this.form.reset();this.getAllLicenses();this.toaster.info("::successfulyUploaded");}),
    ).subscribe()    
  }


  //#region methods
  formatExpirationDateToShowing(dateTime){
    const yourDateTime = new Date(dateTime); // Example DateTime
    const formattedDate = 
      yourDateTime.getFullYear() + '-' +
      ('0' + (yourDateTime.getMonth() + 1)).slice(-2) + '-' + // Month
      ('0' + yourDateTime.getDate()).slice(-2) ; // Day

      return formattedDate;
  }
  //#endregion





}
