import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  template: `<app-host-dashboard *abpPermission="'EduZone.Dashboard.Host'"></app-host-dashboard>
            <app-tenant-dashboard *abpPermission="'EduZone.Dashboard.Tenant'"></app-tenant-dashboard>`
})
export class DashboardComponent {

}
