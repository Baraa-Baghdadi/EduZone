import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { SharedComponentRoutingModule } from './shared-component-routing.module';
import { EnrollmentDetailsComponent } from './enrollment-details/enrollment-details.component';

@NgModule({
  declarations: [
    EnrollmentDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedComponentRoutingModule,
    SharedModule
  ],
  exports:[
    EnrollmentDetailsComponent
  ]
})
export class SharedComponentModule { }
