import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InstructorInfoRoutingModule } from './instructor-info-routing.module';
import { InstructorInfoComponent } from './instructor-info/instructor-info.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    InstructorInfoComponent
  ],
  imports: [
    CommonModule,
    InstructorInfoRoutingModule,
    SharedModule
  ]
})
export class InstructorInfoModule { }
