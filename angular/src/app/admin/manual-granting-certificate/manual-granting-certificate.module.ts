import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ManualGrantingCertificateRoutingModule } from './manual-granting-certificate-routing.module';
import { CreateComponent } from './create/create.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ListComponent } from './list/list.component';


@NgModule({
  declarations: [
    CreateComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    ManualGrantingCertificateRoutingModule,
    SharedModule
  ]
})
export class ManualGrantingCertificateModule { }
