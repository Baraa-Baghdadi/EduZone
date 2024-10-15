import { ToasterService } from '@abp/ng.theme.shared';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GenerateCertificateService } from '@proxy/generate-certificate';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrl: './create.component.scss',
  animations:[trigger('fade',[ 
    state('void',style({opacity:0})), 
    transition(':enter',[ animate(500) ]) 
  ]) 
]
})
export class CreateComponent implements OnInit {
  form:FormGroup;
  selected?: any;
  /**
   *
   */
  constructor(private service : GenerateCertificateService,private fb:FormBuilder,
    private toaster:ToasterService) {
  }

  ngOnInit(): void {
    this.buildForm();
  }

  buildForm(){
    const {
      studentName,
      courseName,
      date
    } = this.selected || {};

    this.form = this.fb.group({
      studentName : [studentName ?? '' ,Validators.required],
      courseName : [courseName ?? null, Validators.required],
      date : [date ?? null, Validators.required]
    });
  }

  
}
