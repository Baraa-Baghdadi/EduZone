import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyCoursesRoutingModule } from './my-courses-routing.module';
import { MyCoursesComponent } from './my-courses/my-courses.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { CreateCourseComponent } from './create-course/create-course.component';
import { initializeApp } from "firebase/app";
import { environment } from 'src/environments/environment';
import { LessonsListComponent } from './lessons-list/lessons-list.component';
import { ThumbnailComponent } from '../shared/thumbnail/thumbnail.component';
import { RelativeTimePipe } from 'src/app/pipe/relative-time.pipe';

initializeApp(environment.firebase);
@NgModule({
  declarations: [
    MyCoursesComponent,
    CreateCourseComponent,
    LessonsListComponent,
    RelativeTimePipe
  ],
  imports: [
    CommonModule,
    MyCoursesRoutingModule,
    SharedModule,
    ThumbnailComponent
    
  ]
})
export class MyCoursesModule { }
