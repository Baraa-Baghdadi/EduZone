import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

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
    path: 'all-instructor',
    loadChildren: () => import('./admin/instructors/instructors.module').then(m => m.InstructorsModule),
  },
  {
    path: 'all-student',
    loadChildren: () => import('./admin/students/students.module').then(m => m.StudentsModule),
  },
  {
    path: 'my-student',
    loadChildren: () => import('./instructor/my-student/my-student.module').then(m => m.MyStudentModule),
  },
  {
    path: 'my-courses',
    loadChildren: () => import('./instructor/my-courses/my-courses.module').then(m => m.MyCoursesModule),
  },
  {
    path: 'courses-Rating',
    loadChildren: () => import('./instructor/instructor-ratings/instructor-ratings.module').then(m => m.InstructorRatingsModule),
  },
  {
    path: 'instructor-Info',
    loadChildren: () => import('./instructor/instructor-info/instructor-info.module').then(m => m.InstructorInfoModule),
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
