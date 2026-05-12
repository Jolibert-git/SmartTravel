import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ReservationService } from '../../core/services/api.services';
import { Reservation } from '../../core/models/models';

@Component({
  selector: 'app-reservations',
  standalone: true,
  imports: [CommonModule, RouterLink, MatSnackBarModule],
  templateUrl: './reservations.component.html',
  styleUrl: './reservations.component.css'
})
export class ReservationsComponent implements OnInit {
  reservations = signal<Reservation[]>([]);
  loading = signal(true);

  constructor(
    private reservationService: ReservationService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadReservations();
  }

  loadReservations(): void {
    this.loading.set(true);
    this.reservationService.getMyReservations().subscribe({
      next: (data) => {
        this.reservations.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
        this.snackBar.open('Error loading reservations', 'Close', {
          duration: 4000,
          panelClass: ['snack-error'],
        });
      },
    });
  }

  cancel(id: number): void {
    this.reservationService.cancel(id).subscribe({
      next: () => {
        this.snackBar.open('Reservation cancelled', 'Close', {
          duration: 3000,
          panelClass: ['snack-success'],
        });
        this.loadReservations();
      },
      error: () => {
        this.snackBar.open('Error cancelling reservation', 'Close', {
          duration: 4000,
          panelClass: ['snack-error'],
        });
      },
    });
  }

  getStatusClass(status: string | undefined): string {
    switch (status?.toLowerCase()) {
      case 'confirmed': return 'badge-confirmed';
      case 'pending':   return 'badge-pending';
      case 'cancelled': return 'badge-cancelled';
      case 'completed': return 'badge-completed';
      default:          return 'badge-pending';
    }
  }
}

