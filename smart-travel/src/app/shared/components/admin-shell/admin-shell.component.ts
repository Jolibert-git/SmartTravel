import { Component, computed } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/services/auth.service';

interface AdminNavItem {
  label: string;
  route: string;
  icon: string;
}

@Component({
  selector: 'app-admin-shell',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './admin-shell.component.html',
  styleUrl: './admin-shell.component.css'
})
export class AdminShellComponent {
  sidenavOpen = true;

  navItems: AdminNavItem[] = [
    { label: 'Dashboard',   route: '/admin/dashboard',  icon: 'dashboard'  },
    { label: 'Suppliers',   route: '/admin/suppliers',  icon: 'suppliers'  },
    { label: 'Services',    route: '/admin/services',   icon: 'services'   },
    { label: 'Users',       route: '/admin/users',      icon: 'users'      },
    { label: 'Promotions',  route: '/admin/promotions', icon: 'promotions' },
  ];

  currentUser = computed(() => this.authService.currentUser());
  userInitials = computed(() => {
    const user = this.authService.currentUser();
    if (!user) return '?';
    return `${user.firstName[0]}${user.lastName[0]}`.toUpperCase();
  });

  constructor(private authService: AuthService) {}

  toggleSidenav(): void {
    this.sidenavOpen = !this.sidenavOpen;
  }

  goToApp(): void {
    window.location.href = '/app/reservations';
  }

  logout(): void {
    this.authService.logout();
  }
}