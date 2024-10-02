import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyCoursesComponent } from './my-courses/my-courses.component';
import { CreateCourseComponent } from './create-course/create-course.component';
import { LessonsListComponent } from './lessons-list/lessons-list.component';
const routes: Routes = [
  {path:"",component:MyCoursesComponent},
  {path:"createCourse",component:CreateCourseComponent},
  {path:"lessons-list/:id",component:LessonsListComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MyCoursesRoutingModule { }
