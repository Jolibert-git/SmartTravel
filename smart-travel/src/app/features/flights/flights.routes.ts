import { Routes } from '@angular/router';

export const FLIGHT_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./flights.component').then((m) => m.FlightsComponent),
  },
];