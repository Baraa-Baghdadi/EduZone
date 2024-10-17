import { getLocaleDirection, LocalizationService, SessionStateService } from '@abp/ng.core';
import { LocaleDirection } from '@abp/ng.theme.shared';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { genderOptions } from '@proxy/enum';
import { InstructorAuthService } from '@proxy/instructors-auth';
import { BehaviorSubject, map } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
  animations:[trigger('fade',[ 
    state('void',style({opacity:0})), 
    transition(':enter',[ animate(500) ]) 
  ]) 
]
})
export class RegisterComponent implements OnInit {
  active = 1;
  // for languages:
  availableLangs = [{value:"en",name:"English"},{value:"ar",name:"العربية"}];
  selectedLang = this.sessionState.getLanguage();
  labelOfSelectedLang = this.selectedLang === "en" ? "English" : "العربية" ;
  showLangList = false;
  private dir = new BehaviorSubject<LocaleDirection>('ltr');
  dir$ = this.dir.asObservable();

  // form:
  form:FormGroup;
  genders = genderOptions;

  // complete regster:
  showRegisterPage = false ;


  /**
   *
   */
  constructor(private fb:FormBuilder,
    private sessionState:SessionStateService,
    private localizationService:LocalizationService,
    private authService:InstructorAuthService) {
      this.listenToLanguageChanges();
  }

  ngOnInit(): void {
    this.buildForm();
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

  buildForm(){
    this.form = this.fb.group({
      firstName : [null ,Validators.required],
      lastName : [ null ,Validators.required],
      gender : [null, Validators.required],
      email : [null , Validators.required],
      password : [null , Validators.required],
      confirmPassword : [null ,Validators.required],
      about : [null ,Validators.required],
    },{ validator: this.passwordMatchValidator});
  }

  passwordMatchValidator(form: FormGroup) {
    return form.get('password')?.value === form.get('confirmPassword')?.value
      ? null : { mismatch: true };
  }

  submit(){
    if (this.form.valid) {
      this.authService.createNewInstructorByInput(this.form.value).subscribe((data) =>{     
        if (data = true) {
          this.showRegisterPage = true;
        }
      });
    }
  }
}
