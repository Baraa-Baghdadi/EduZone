import { ConfigStateService, getLocaleDirection, LocalizationService, SessionStateService } from '@abp/ng.core';
import { LocaleDirection } from '@abp/ng.theme.shared';
import { Component, ElementRef, HostListener, OnInit } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { NotificationListenerService } from 'src/app/services/notifications/notification-listener.service';

@Component({
  selector: 'app-new-header',
  templateUrl: './new-header.component.html',
  styleUrl: './new-header.component.scss'
})
export class NewHeaderComponent implements OnInit {
  availableLangs = [{value:"en",name:"English"},{value:"ar",name:"العربية"}];
  selectedLang = this.sessionState.getLanguage();
  labelOfSelectedLang = this.selectedLang === "en" ? "English" : "العربية" ;
  showLangList = false;
  isShowListOfNotification = false;

  private dir = new BehaviorSubject<LocaleDirection>('ltr');
  dir$ = this.dir.asObservable();

  allMsgs : any;
  // For pagination:
  isLoading=false;
  currentPage=1;
  itemsPerPage=10;
  toggleLoading = ()=>this.isLoading=!this.isLoading;

  // it will be called when this component gets initialized.
  loadData= ()=>{
    this.toggleLoading();
    this.getMsgList(this.currentPage,this.itemsPerPage);
  }
  
  // this method will be called on scrolling the page
  appendData= ()=>{
    this.toggleLoading();
    this.getMsgList(this.currentPage,this.itemsPerPage);
  }

  onScroll= ()=>{
    this.currentPage++;     
    this.appendData();
  }

  showMsgList(){
    this.isShowListOfNotification = true;
    this.makeAllMsgAsReaded();
  }


  constructor(
    private config: ConfigStateService,
    private sessionState:SessionStateService,
    public localizationService : LocalizationService,
    public notificationListener : NotificationListenerService,
    private elementRef: ElementRef) { 
      this.listenToLanguageChanges();
    }

    ngOnInit() {
      const tenantId = this.config.getOne("currentUser").tenantId;
      if (tenantId) {  
        this.getUnreadedMsg();
        this.loadData();
      }
    }
    
    // For Select language:
   selectLang(lang:any){
      this.sessionState.setLanguage(lang);
      this.selectedLang = this.sessionState.getLanguage();
      this.labelOfSelectedLang = this.selectedLang === "en" ? "English" : "العربية" ;
      this.showLangList = false;  
    }
  
    private listenToLanguageChanges(){
      this.localizationService.currentLang$.pipe(map(locale => getLocaleDirection(locale))).subscribe(dir => {
        this.dir.next(dir);
        this.setBodyDir(dir);
        this.notificationListener.makeNotificationListEmpty();
      })
    }
  
    private setBodyDir(dir : LocaleDirection){
      document.body.dir = dir;
      document.dir = dir;
    }




    // Close PopUp:
    @HostListener('document:click', ['$event.target'])
    public onClick(targetElement: HTMLElement) {
      const tenantId = this.config.getOne("currentUser").tenantId;
      if (tenantId) {
        const clickedInside = this.elementRef.nativeElement.contains(targetElement);
        if (!clickedInside) {
          this.isShowListOfNotification = false;
        }
      }
    }


  // For Notifications:
  getUnreadedMsg(){
  this.notificationListener.getUnreadedMsg();         
  }

  makeAllMsgAsReaded(){
    this.notificationListener.makeAllMsgAsReaded();
  }

  getMsgList(page=1,itemsPerPage=10){
    this.notificationListener.getMsgList(page,itemsPerPage);
  }


}
