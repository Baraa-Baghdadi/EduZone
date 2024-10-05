import { PagedResultDto } from '@abp/ng.core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { GetCourseRatingOfInstructor, RateDto, RateService } from '@proxy/ratings';
import { debounceTime, distinctUntilChanged, fromEvent, map, switchMap, tap } from 'rxjs';
import { IRatingOptions } from 'ngx-stars-rating';
@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrl: './list.component.scss',
  animations:[trigger('fade',[ 
    state('void',style({opacity:0})), 
    transition(':enter',[ animate(500) ]) 
  ]) 
]
})
export class ListComponent implements OnInit {
  @ViewChild('search') search !: ElementRef ;
  title = 'type a head search';
  ratingsFilter = {} as GetCourseRatingOfInstructor;
  ratings = { items: [], totalCount: 0 } as PagedResultDto<RateDto>;
  state = false ;

  public ratingOptions: any = {
    hoverable: true,
    clickable: false
};


  constructor(private service : RateService) { 
  }

  ngOnInit() {
    this.getAllRatings();
  }

  getAllRatings(){
    this.service.getRateMyCourseByInput(this.ratingsFilter).subscribe({
      next : (data:any) => {this.ratings = data;console.log(data);
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
         this.ratings = { items: [], totalCount: 0 };
         this.getAllRatings();
         return [];
        }
        return this.service.getRateMyCourseByInput(this.ratingsFilter);
      }),
      tap(()=>this.state =false)
    ).subscribe((data:any)=> this.ratings = data)
  }
    
}
