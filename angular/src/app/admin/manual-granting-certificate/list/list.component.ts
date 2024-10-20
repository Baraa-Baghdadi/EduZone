import { PagedResultDto } from '@abp/ng.core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { CertificateDto, CertificateService, GetCertificatesInput } from '@proxy/certificates';
import { debounceTime, distinctUntilChanged, fromEvent, map, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrl: './list.component.scss',
  animations:[trigger('fade',[ 
    state('void',style({opacity:0})), 
    transition(':enter',[ animate(500) ]) 
  ]) 
]
})
export class ListComponent implements OnInit {
  @ViewChild('search') search !: ElementRef ;
  title = 'type a head search';
  certificateFilter = {} as GetCertificatesInput;
  certificates = { items: [], totalCount: 0 } as PagedResultDto<CertificateDto>;
  state = false ;

  // paggination:
  page = {pageNumber:0,size:10};
    /**
   *
   */
    constructor(private service : CertificateService) { 
    }

    ngOnInit() {
      this.setPagginationDefault();
      this.getAllCertificates()
    }
  
    getAllCertificates(){
      this.service.getAllCertificatesByInput(this.certificateFilter).subscribe({
        next : (data:any) => {this.certificates = data;console.log(data);}
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
           this.certificates = { items: [], totalCount: 0 };
           this.goToFirstPage();
           this.getAllCertificates();
           return [];
          }
          this.goToFirstPage();
          return this.service.getAllCertificatesByInput(this.certificateFilter);
        }),
        tap(()=>this.state =false)
      ).subscribe((data:any)=> this.certificates = data)
    }

    downloadCertificate(id){
      this.service.adminDownlaodCertificateById(id).subscribe(res => {
        const blob = new Blob([res],{type:"application/pdf"});
        const link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = "Certificate.pdf";
        link.click();
      }
    )
  }

   // For Paggination:
   setPagginationDefault(){
    this.page.pageNumber = 0;
    this.page.size = 10;
    this.setPage({offset:0});
  }
  setPage(pageInfo){
    this.page.pageNumber = pageInfo.offset;
    this.certificateFilter.skipCount = this.page.pageNumber * this.page.size;
    this.getAllCertificates();
  }

  goToFirstPage(){
    this.certificateFilter.skipCount = 0;
    this.page.pageNumber = 0;
  }
}
