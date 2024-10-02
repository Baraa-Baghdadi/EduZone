import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InstructorsRoutingModule } from './instructors-routing.module';
import { AllInstructorsComponent } from './all-instructors/all-instructors.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    AllInstructorsComponent
  ],
  imports: [
    CommonModule,
    InstructorsRoutingModule,
    SharedModule
  ]
})
export class InstructorsModule { }
