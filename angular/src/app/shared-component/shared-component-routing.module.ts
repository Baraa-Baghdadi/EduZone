import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EnrollmentDetailsComponent } from './enrollment-details/enrollment-details.component';

const routes: Routes = [
    {path:"enrollment-details/:id",component:EnrollmentDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SharedComponentRoutingModule {}