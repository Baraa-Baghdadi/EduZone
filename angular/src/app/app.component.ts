import { AuthService, ReplaceableComponentsService } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { eThemeLeptonXComponents } from '@abp/ng.theme.lepton-x';
import { NewHeaderComponent } from './welcome/new-header/new-header.component';

@Component({
  selector: 'app-root',
  template: `
    <abp-loader-bar></abp-loader-bar>
    <abp-dynamic-layout></abp-dynamic-layout>
    <abp-internet-status></abp-internet-status>
  `,
})
export class AppComponent implements OnInit {

  constructor(private authService:AuthService,
    private replaceableComponent: ReplaceableComponentsService,
  ){
  }
  ngOnInit(): void {
    this.replaceableComponent.add({
      component: NewHeaderComponent,
      key: eThemeLeptonXComponents.Languages,
    });
}
}
