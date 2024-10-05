import { ToasterService } from '@abp/ng.theme.shared';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Gender, genderOptions } from '@proxy/enum';
import { InstructorService } from '@proxy/instructors';

@Component({
  selector: 'app-instructor-info',
  templateUrl: './instructor-info.component.html',
  styleUrl: './instructor-info.component.scss',
  animations:[trigger('fade',[ 
    state('void',style({opacity:0})), 
    transition(':enter',[ animate(500) ]) 
  ]) 
]
})
export class InstructorInfoComponent implements OnInit {
  form:FormGroup;
  selected?: any;
  genders = genderOptions;
  constructor( private service : InstructorService,private fb:FormBuilder,private toaster:ToasterService) {
  }
  ngOnInit() {
    this.buildForm();
    this.getProviderInfo(); 
  }

  buildForm(){
    const {
      firstName,
      lastName,
      gender,
      about
    } = this.selected || {};

    this.form = this.fb.group({
      firstName : [firstName ?? '' ,Validators.required],
      lastName : [lastName ?? null ,Validators.required],
      gender : [gender ?? null, Validators.required],
      about : [about ?? null, Validators.required]
    });
  }

  save(){
    if(this.form.valid){
      this.service.updateInstructorInfoByInput(this.form.value).subscribe(data =>{
        this.toaster.info("::successfullyUpdated");
      });
    }
  }

  getProviderInfo(){
    this.service.getInstructorInfo().subscribe((data:any) => {
      this.selected = data;
      this.buildForm();
    });
  }
}
