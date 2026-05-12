import { Component, computed } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/services/auth.service';

interface NavItem {
  label: string;
  route: string;
  icon: string;
}

@Component({
  selector: 'app-shell',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './shell.component.html',
  styleUrl: './shell.component.css'
})
export class ShellComponent {
  sidenavOpen = true;

  navItems: NavItem[] = [
    { label: 'Reservations', route: '/app/reservations', icon: 'calendar' },
    { label: 'Flights',      route: '/app/flights',      icon: 'flight'    },
    { label: 'Hotels',       route: '/app/hotels',       icon: 'hotel'     },
    { label: 'Vehicles',     route: '/app/vehicles',     icon: 'vehicle'   },
    { label: 'Packages',     route: '/app/packages',     icon: 'package'   },
    { label: 'Payments',     route: '/app/payments',     icon: 'payment'   },
    { label: 'Passengers',   route: '/app/passengers',   icon: 'passengers'},
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

  logout(): void {
    this.authService.logout();
  }
}