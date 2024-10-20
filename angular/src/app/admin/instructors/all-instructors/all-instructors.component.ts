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

    // paggination:
    page = {pageNumber:0,size:10};

  constructor(private service : InstructorService){}

  ngOnInit() {
    this.setPagginationDefault();
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
         this.goToFirstPage();
         this.getAllInstructors();
         return [];
        }
        this.goToFirstPage();
        return this.service.getAllInstructorByInput(this.instructorFilter);
      }),
      tap(()=>this.state =false)
    ).subscribe((data:any)=> this.instructors = data)
  }

  showDetailsOfInstructor(rowId){
  }

    // For Paggination:
    setPagginationDefault(){
      this.page.pageNumber = 0;
      this.page.size = 10;
      this.setPage({offset:0});
    }
    setPage(pageInfo){
      this.page.pageNumber = pageInfo.offset;
      this.instructorFilter.skipCount = this.page.pageNumber * this.page.size;
      this.getAllInstructors();
    }
  
    goToFirstPage(){
      this.instructorFilter.skipCount = 0;
      this.page.pageNumber = 0;
    }

}
