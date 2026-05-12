import { Routes } from '@angular/router';

export const PACKAGE_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./packages.component').then((m) => m.PackagesComponent),
  },
];