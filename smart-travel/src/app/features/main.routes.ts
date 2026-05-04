import { Routes } from '@angular/router';

export const MAIN_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./shared/components/shell/shell.component').then(
        (m) => m.ShellComponent
      ),
    children: [
      { path: '', redirectTo: 'reservations', pathMatch: 'full' },
      {
        path: 'reservations',
        loadChildren: () =>
          import('./features/reservations/reservations.routes').then(
            (m) => m.RESERVATION_ROUTES
          ),
      },
      {
        path: 'flights',
        loadChildren: () =>
          import('./features/flights/flights.routes').then(
            (m) => m.FLIGHT_ROUTES
          ),
      },
      {
        path: 'hotels',
        loadChildren: () =>
          import('./features/hotels/hotels.routes').then(
            (m) => m.HOTEL_ROUTES
          ),
      },
      {
        path: 'packages',
        loadChildren: () =>
          import('./features/packages/packages.routes').then(
            (m) => m.PACKAGE_ROUTES
          ),
      },
      {
        path: 'passengers',
        loadChildren: () =>
          import('./features/passengers/passengers.routes').then(
            (m) => m.PASSENGER_ROUTES
          ),
      },
      {
        path: 'payments',
        loadChildren: () =>
          import('./features/payments/payments.routes').then(
            (m) => m.PAYMENT_ROUTES
          ),
      },
    ],
  },
];