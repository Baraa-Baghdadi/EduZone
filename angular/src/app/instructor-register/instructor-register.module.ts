import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { InstructorRegisterRoutingModule } from './instructor-register-routing.module';
import { RegisterComponent } from './register/register.component';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [
    RegisterComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    NgbDropdownModule,
    InstructorRegisterRoutingModule
  ]
})
export class InstructorRegisterModule { }
