import { PagedResultDto } from '@abp/ng.core';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { GetStudentInput, StudentDto, StudentService } from '@proxy/students';
import { debounceTime, distinctUntilChanged, fromEvent, map, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-all-students',
  templateUrl: './all-students.component.html',
  styleUrl: './all-students.component.scss'
})
export class AllStudentsComponent {
  @ViewChild('search') search !: ElementRef ;
  title = 'type a head search';
  studentsFilter = {} as GetStudentInput;
  students = { items: [], totalCount: 0 } as PagedResultDto<StudentDto>;
  state = false ;

  isModalOpen =false;

  constructor(private service : StudentService){}

  ngOnInit() {
    this.getAllStudent();
  }

  getAllStudent(){
    this.service.getAllStudentByInput(this.studentsFilter).subscribe({
      next : (data:any) => {this.students = data;console.log(data);
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
         this.students = { items: [], totalCount: 0 };
         this.getAllStudent();
         return [];
        }
        return this.service.getAllStudentByInput(this.studentsFilter);
      }),
      tap(()=>this.state =false)
    ).subscribe((data:any)=> this.students = data)
  }

  showDetailsOfInstructor(rowId){
  }
}
