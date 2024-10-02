import { AuthService } from '@abp/ng.core';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CategoryService } from '@proxy/categories';
import { CourseService } from '@proxy/courses';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent {
  get hasLoggedIn(): boolean {
    return this.authService.isAuthenticated;
  }

  constructor(private authService: AuthService , 
    private router:Router,
    private service : CourseService,
    private categoryServ:CategoryService) {
    if (!this.authService.isAuthenticated) this.router.navigateByUrl("/welcome");
  }

  login() {
    this.authService.navigateToLogin();
  }

 

  
}

