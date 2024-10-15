import { LocaleDirection } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { AuthService, getLocaleDirection, LocalizationService, SessionStateService } from '@abp/ng.core';
import { BehaviorSubject, map } from 'rxjs';
import { InstructorAuthService } from '@proxy/instructors-auth';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-email-confirmation',
  templateUrl: './email-confirmation.component.html',
  styleUrl: './email-confirmation.component.scss'
})
export class EmailConfirmationComponent implements OnInit {
    // for languages:
    availableLangs = [{value:"en",name:"English"},{value:"ar",name:"العربية"}];
    selectedLang = this.sessionState.getLanguage();
    labelOfSelectedLang = this.selectedLang === "en" ? "English" : "العربية" ;
    showLangList = false;
    private dir = new BehaviorSubject<LocaleDirection>('ltr');
    dir$ = this.dir.asObservable();


    // main var:
    showSuccessMsg = false;
    userEmail = '';
    emailVerfication = '';
    resultCalculated = false;

  
    /**
     *
     */
    constructor(
      private route : ActivatedRoute,
      private auth : AuthService,
      private sessionState:SessionStateService,
      private localizationService:LocalizationService,
      private authService:InstructorAuthService) {
        this.listenToLanguageChanges();
    }

    ngOnInit() {
      this.route.queryParams.subscribe(params => {
        this.userEmail = params['u'];
        this.emailVerfication = params['c'];
        this.verifyEmail();
      });
    }

    verifyEmail(){
      this.authService.verifyByInput({email:this.userEmail,token:this.emailVerfication}).subscribe(res => {
        if (res) {
          this.showSuccessMsg = true ;
        }
        else{
          this.showSuccessMsg = false;
          this.resultCalculated = true;
        }
      });
    }

    login(){
      this.auth.navigateToLogin();
    }

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
      })
    }
  
    private setBodyDir(dir : LocaleDirection){
      document.body.dir = dir;
      document.dir = dir;
    }
}
