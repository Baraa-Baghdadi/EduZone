import { getLocaleDirection, LocalizationService, SessionStateService } from '@abp/ng.core';
import { LocaleDirection } from '@abp/ng.theme.shared';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { AfterViewInit, Component, ElementRef, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CountryCodeService } from '@proxy/country-codes';
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

  // complete register:
  showRegisterPage = false ;

  // codes:
  defaultCode = "+963";
  defaultFlag = "https://flagsapi.com/SY/flat/24.png";
  countryCodes = [];
  selectedOption: string = '';
  isOpen: boolean = false;

  /**
   *
   */
  constructor(private fb:FormBuilder,
    private sessionState:SessionStateService,
    private localizationService:LocalizationService,
    private authService:InstructorAuthService,
    private countryCodeService : CountryCodeService,
    private elementRef: ElementRef ) {
      this.listenToLanguageChanges();
  }

  ngOnInit(): void {
    this.getCountryCode();
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
      license : [null,Validators.required,Validators.maxLength(6)],
      firstName : [null ,Validators.required,Validators.maxLength(10)],
      lastName : [ null ,Validators.required,Validators.maxLength(15)],
      gender : [null, Validators.required],
      email : [null , Validators.required],
      password : [null , Validators.required,Validators.minLength(6)],
      confirmPassword : [null ,Validators.required,Validators.minLength(6)],
      about : [null],
      countryCode : [this.defaultCode,Validators.required],
      mobileNumber : [null,Validators.required]
    },{ validator: this.passwordMatchValidator});
  }

  // match password and confirm validation:
  passwordMatchValidator(form: FormGroup) {
    return form.get('password')?.value === form.get('confirmPassword')?.value
      ? null : { mismatch: true };
  }

  get countryCode(){
    return this.form.get('countryCode') as FormControl;
  }

  getCountryCode(){
    this.countryCodeService.getCountryCodes().subscribe((data:any) => {
      this.countryCodes = data;
    })
  }

  // custom dropdown for mobile number:

  toggleDropdown() {
    this.isOpen = !this.isOpen;    
  }

  selectOption(option: string) {
    this.selectedOption = option;
    this.countryCode.setValue(option);
    this.isOpen = false;
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