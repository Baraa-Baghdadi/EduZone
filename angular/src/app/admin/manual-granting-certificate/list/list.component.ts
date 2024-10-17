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

    /**
   *
   */
    constructor(private service : CertificateService) { 
    }

    ngOnInit() {
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
           this.getAllCertificates();
           return [];
          }
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
}
