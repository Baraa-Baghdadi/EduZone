import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllInstructorsComponent } from './all-instructors/all-instructors.component';

const routes: Routes = [
  {path:'',component:AllInstructorsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InstructorsRoutingModule { }
