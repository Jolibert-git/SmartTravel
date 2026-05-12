import { Routes } from '@angular/router';

export const RESERVATION_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./reservations.component').then((m) => m.ReservationsComponent),
  },
];

