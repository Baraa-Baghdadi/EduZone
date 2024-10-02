import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyStudentComponent } from './my-student/my-student.component';

const routes: Routes = [
  {path:"",component:MyStudentComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MyStudentRoutingModule { }
