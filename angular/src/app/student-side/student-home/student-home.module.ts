import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StudentHomeRoutingModule } from './student-home-routing.module';
import { HomeComponent } from './home/home.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    StudentHomeRoutingModule,
    SharedModule
  ]
})
export class StudentHomeModule { }
