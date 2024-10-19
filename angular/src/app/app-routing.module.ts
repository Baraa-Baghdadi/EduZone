import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmailConfirmationComponent } from './email-confirm/email-confirmation/email-confirmation.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'welcome',
    loadChildren: () => import('./welcome/welcome.module').then(m => m.WelcomeModule),
  },
  {
    path: 'register',
    loadChildren: () => import('./instructor-register/instructor-register.module').then(m => m.InstructorRegisterModule),
  },
  {
    path:"email-confirmation",
    component : EmailConfirmationComponent
  },
  {
    path: 'all-instructor',
    loadChildren: () => import('./admin/instructors/instructors.module').then(m => m.InstructorsModule),
    canActivate:[authGuard,permissionGuard]
  },
  {
    path: 'all-student',
    loadChildren: () => import('./admin/students/students.module').then(m => m.StudentsModule),
    canActivate:[authGuard,permissionGuard]
  },
  {
    path: 'my-student',
    loadChildren: () => import('./instructor/my-student/my-student.module').then(m => m.MyStudentModule),
    canActivate:[authGuard,permissionGuard]
  },
  {
    path: 'my-courses',
    loadChildren: () => import('./instructor/my-courses/my-courses.module').then(m => m.MyCoursesModule),
    canActivate:[authGuard,permissionGuard]
  },
  {
    path: 'courses-Rating',
    loadChildren: () => import('./instructor/instructor-ratings/instructor-ratings.module').then(m => m.InstructorRatingsModule),
    canActivate:[authGuard,permissionGuard]
  },
  {
    path: 'instructor-Info',
    loadChildren: () => import('./instructor/instructor-info/instructor-info.module').then(m => m.InstructorInfoModule),
    canActivate:[authGuard,permissionGuard]
  },
  {
    path: 'students-Certificates',
    loadChildren: () => import('./admin/manual-granting-certificate/manual-granting-certificate.module').then(m => m.ManualGrantingCertificateModule),
    canActivate:[authGuard,permissionGuard]
  },
  {
    path: 'my-students-Certificates',
    loadChildren: () => import('./instructor/my-student-certificates/my-student-certificates.module').then(m => m.MyStudentCertificatesModule),
    canActivate:[authGuard,permissionGuard]
  },
  {
    path: 'license-managment',
    loadChildren: () => import('./admin/license/license.module').then(m => m.LicenseModule),
    canActivate:[authGuard,permissionGuard]
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(m => m.AccountModule.forLazy()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'tenant-management',
    loadChildren: () =>
      import('@abp/ng.tenant-management').then(m => m.TenantManagementModule.forLazy()),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
