import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/welcome',
        name: 'welcome',
        order: 0,
        layout: eLayoutType.empty,
        invisible : true
      },
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/all-instructor',
        name: '::Menu:Instructors',
        iconClass: 'fas fa-user-circle',
        order: 2,
        requiredPolicy: 'EduZone.Dashboard.Host',
        layout: eLayoutType.application,
      },
      {
        path: '/all-student',
        name: '::Menu:Students',
        iconClass: 'fas fa-users',
        order: 3,
        requiredPolicy: 'EduZone.Dashboard.Host',
        layout: eLayoutType.application,
      },
      {
        path: '/my-student',
        name: '::Menu:MyStudents',
        iconClass: 'fas fa-users',
        order: 4,
        requiredPolicy: 'EduZone.Dashboard.Tenant',
        layout: eLayoutType.application,
      },
      {
        path: '/my-courses',
        name: '::Menu:MyCourses',
        iconClass: 'fa fa-university',
        order: 5,
        requiredPolicy: 'EduZone.Dashboard.Tenant',
        layout: eLayoutType.application,
      },
      {
        path: '/courses-Rating',
        name: '::Menu:coursesRating',
        iconClass: 'fa fa-star-o',
        order: 6,
        requiredPolicy: 'EduZone.Dashboard.Tenant',
        layout: eLayoutType.application,
      },
      {
        path: '/instructor-Info',
        name: '::Menu:instructorInfo',
        iconClass: 'fa fa-info-circle',
        order: 7,
        requiredPolicy: 'EduZone.Dashboard.Tenant',
        layout: eLayoutType.application,
      },
    ]);
  };
}
