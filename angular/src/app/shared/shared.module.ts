import { CoreModule } from '@abp/ng.core';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

@NgModule({
  declarations: [],
  imports: [
    CoreModule,
    ThemeSharedModule,
    ReactiveFormsModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
    NgSelectModule,
    FormsModule,
    NgxDatatableModule
  ],
  exports: [
    CoreModule,
    ThemeSharedModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
    NgSelectModule,
    FormsModule,
    NgxDatatableModule
  ],
  providers: []
})
export class SharedModule {}
