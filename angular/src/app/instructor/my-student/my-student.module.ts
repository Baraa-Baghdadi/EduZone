import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MyStudentRoutingModule } from './my-student-routing.module';
import { MyStudentComponent } from './my-student/my-student.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    MyStudentComponent
  ],
  imports: [
    CommonModule,
    MyStudentRoutingModule,
    SharedModule
  ]
})
export class MyStudentModule { }
