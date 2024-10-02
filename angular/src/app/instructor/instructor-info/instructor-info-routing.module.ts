import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InstructorInfoComponent } from './instructor-info/instructor-info.component';

const routes: Routes = [
  {path:"",component:InstructorInfoComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InstructorInfoRoutingModule { }
