import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewHeaderComponent } from './new-header/new-header.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '../shared/shared.module';
import { WelcomeRoutingModule } from './welcome-routing.module';
import { InfiniteScrollModule } from "ngx-infinite-scroll";
@NgModule({
  declarations: [
    WelcomeComponent,
    NewHeaderComponent
  ],
  imports: [
    CommonModule,
    WelcomeRoutingModule,
    NgbDropdownModule,
    SharedModule,
    InfiniteScrollModule
  ]
})
export class WelcomeModule { }
