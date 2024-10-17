import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MyStudentCertificatesRoutingModule } from './my-student-certificates-routing.module';
import { ListComponent } from './list/list.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    ListComponent
  ],
  imports: [
    CommonModule,
    MyStudentCertificatesRoutingModule,
    SharedModule
  ]
})
export class MyStudentCertificatesModule { }
