import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
    firebase: {
      apiKey: "AIzaSyAPohdCTxGXIWAGrAbRSmQnHvr4T6QeVx0",
      authDomain: "notification-94cbd.firebaseapp.com",
      projectId: "notification-94cbd",
      storageBucket: "notification-94cbd.appspot.com",
      messagingSenderId: "360185693171",
      appId: "1:360185693171:web:672b31ddbc97631487c886",
      vapidKey: "BIoFUc-9AjTHTJGOGkyKTXBRFEEnUuFVMb9Bs1P8dZ5Y0RH_t-n4nziMySrfUfvjah8EDcp0UHlpvTYh-dBRZJg"
    },
  application: {
    baseUrl,
    name: 'EduZone',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44366/',
    redirectUri: baseUrl,
    clientId: 'EduZone_App',
    responseType: 'code',
    scope: 'offline_access EduZone',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44366',
      rootNamespace: 'EduZone',
    },
  },
} as Environment;
