import { Routes } from '@angular/router';

export const PAYMENT_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./payments.component').then((m) => m.PaymentsComponent),
  },
];
