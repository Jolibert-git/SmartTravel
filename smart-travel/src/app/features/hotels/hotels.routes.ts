import { Routes } from '@angular/router';

export const HOTEL_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./hotels.component').then((m) => m.HotelsComponent),
  },
];