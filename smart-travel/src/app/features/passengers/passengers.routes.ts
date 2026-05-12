import { Routes } from '@angular/router';

export const PASSENGER_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./passengers.component').then((m) => m.PassengersComponent),
  },
];