import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StudentsRoutingModule } from './students-routing.module';
import { AllStudentsComponent } from './all-students/all-students.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    AllStudentsComponent
  ],
  imports: [
    CommonModule,
    StudentsRoutingModule,
    SharedModule
  ]
})
export class StudentsModule { }
