import { PagedResultDto } from '@abp/ng.core';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { GetLicenseInput, LicenseDto, LicenseService } from '@proxy/licenses';
import { debounceTime, distinctUntilChanged, fromEvent, map, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrl: './list.component.scss'
})
export class ListComponent implements OnInit {
  @ViewChild('search') search !: ElementRef ;
  title = 'type a head search';
  licenseFilter = {} as GetLicenseInput;
  licenses = { items: [], totalCount: 0 } as PagedResultDto<LicenseDto>;
  state = false ;

  isModalOpen =false;

  constructor(private service : LicenseService){}

  ngOnInit() {
    this.getAllLicenses();
  }

  getAllLicenses(){
    this.service.getList(this.licenseFilter).subscribe({
      next : (data:any) => {this.licenses = data;console.log(data);
      }
    });

  }

  ngAfterViewInit(): void {
    fromEvent(this.search.nativeElement,'keyup').pipe(
      debounceTime(100),
      map((evt:any)=>evt.target.value),
      tap(()=>this.state = true) ,
      distinctUntilChanged(),
      switchMap((value:string)=>{
        if (value.length == 0) {
         this.state =false ;
         this.licenses = { items: [], totalCount: 0 };
         this.getAllLicenses();
         return [];
        }
        return this.service.getList(this.licenseFilter);
      }),
      tap(()=>this.state =false)
    ).subscribe((data:any)=> this.licenses = data)
  }

  showDetailsOfLicense(rowId){
  }
}
