import { PagedResultDto } from '@abp/ng.core';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { GetInstructorInput, InstructorDto, InstructorService } from '@proxy/instructors';
import { debounceTime, distinctUntilChanged, fromEvent, map, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-all-instructors',
  templateUrl: './all-instructors.component.html',
  styleUrl: './all-instructors.component.scss'
})
export class AllInstructorsComponent {
  @ViewChild('search') search !: ElementRef ;
  title = 'type a head search';
  instructorFilter = {} as GetInstructorInput;
  instructors = { items: [], totalCount: 0 } as PagedResultDto<InstructorDto>;
  state = false ;

  isModalOpen =false;

  constructor(private service : InstructorService){}

  ngOnInit() {
    this.getAllInstructors();
  }

  getAllInstructors(){
    this.service.getAllInstructorByInput(this.instructorFilter).subscribe({
      next : (data:any) => {this.instructors = data;console.log(data);
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
         this.instructors = { items: [], totalCount: 0 };
         this.getAllInstructors();
         return [];
        }
        return this.service.getAllInstructorByInput(this.instructorFilter);
      }),
      tap(()=>this.state =false)
    ).subscribe((data:any)=> this.instructors = data)
  }

  showDetailsOfInstructor(rowId){
  }

}
