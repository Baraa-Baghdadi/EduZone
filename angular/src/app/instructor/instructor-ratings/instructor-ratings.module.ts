import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InstructorRatingsRoutingModule } from './instructor-ratings-routing.module';
import { ListComponent } from './list/list.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { NgxStarsRatingModule } from 'ngx-stars-rating';

@NgModule({
  declarations: [
    ListComponent
  ],
  imports: [
    CommonModule,
    InstructorRatingsRoutingModule,
    SharedModule,
    NgxStarsRatingModule
  ]
})
export class InstructorRatingsModule { }
