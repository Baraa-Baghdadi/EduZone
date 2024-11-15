import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EnrollmentDto, EnrollmentService } from '@proxy/enrollments';

@Component({
  selector: 'app-enrollment-details',
  templateUrl: './enrollment-details.component.html',
  styleUrl: './enrollment-details.component.scss',
  animations:[trigger('fade',[ 
    state('void',style({opacity:0})), 
    transition(':enter',[ animate(500) ]) 
  ]) 
]
})
export class EnrollmentDetailsComponent implements OnInit {
  enrollment = {} as EnrollmentDto;
  /**
   *
   */
  constructor(
    private activatedRout : ActivatedRoute,
    private enrollService : EnrollmentService
  ){   
  }

  ngOnInit() {
    this.getDataFromUrl();
  }
  getDataFromUrl(){
    this.activatedRout.params.subscribe((data)=>{
      if (data.id && data.id!= undefined) {
        this.getEnrollDetails(data.id);
      }
      else{
        return;
      }
    })
  }
  getEnrollDetails(id){
    this.enrollService.getEnrollmentByIdById(id).subscribe(result => {
      this.enrollment = result;
    });
  }
}
