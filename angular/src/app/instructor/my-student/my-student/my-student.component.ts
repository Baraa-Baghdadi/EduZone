import { PagedResultDto } from '@abp/ng.core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { EnrollmentDto, EnrollmentService, GetEnrollmentInput } from '@proxy/enrollments';
import { debounceTime, distinctUntilChanged, fromEvent, map, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-my-student',
  templateUrl: './my-student.component.html',
  styleUrl: './my-student.component.scss',
  animations:[trigger('fade',[ 
    state('void',style({opacity:0})), 
    transition(':enter',[ animate(500) ]) 
  ]) 
]
})
export class MyStudentComponent implements OnInit,AfterViewInit {
  @ViewChild('search') search !: ElementRef ;
  title = 'type a head search';
  enrollmentFilter = {} as GetEnrollmentInput;
  enrollments = { items: [], totalCount: 0 } as PagedResultDto<EnrollmentDto>;
  state = false ;

  /**
   *
   */
  constructor(private service : EnrollmentService) { 
  }

  ngOnInit() {
    this.getAllEnrollments()
  }

  getAllEnrollments(){
    this.service.getEnrollmentsOfInstructorByInput(this.enrollmentFilter).subscribe({
      next : (data:any) => {this.enrollments = data;console.log(data);
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
         this.enrollments = { items: [], totalCount: 0 };
         this.getAllEnrollments();
         return [];
        }
        return this.service.getEnrollmentsOfInstructorByInput(this.enrollmentFilter);
      }),
      tap(()=>this.state =false)
    ).subscribe((data:any)=> this.enrollments = data)
  }
}
