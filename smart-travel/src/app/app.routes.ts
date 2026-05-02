import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { adminGuard } from './core/guards/admin.guard';

export const routes: Routes = [
  // Redirigir raíz al login
  { path: '', redirectTo: 'auth/login', pathMatch: 'full' },

  // Auth — público, no requiere login
  {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/auth.routes').then((m) => m.AUTH_ROUTES),
  },

  // App principal — requiere login
  {
    path: 'app',
    canActivate: [authGuard],
    loadChildren: () =>
      import('./features/main.routes').then((m) => m.MAIN_ROUTES),
  },

  // Panel admin — requiere login + rol Admin o Agente
  {
    path: 'admin',
    canActivate: [authGuard, adminGuard],
    loadChildren: () =>
      import('./admin/admin.routes').then((m) => m.ADMIN_ROUTES),
  },

  // Cualquier ruta no encontrada → login
  { path: '**', redirectTo: 'auth/login' },
];