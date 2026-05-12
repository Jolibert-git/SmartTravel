import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { HotelService, CatalogService } from '../../core/services/api.services';
import { Hotel, Destination } from '../../core/models/models';

@Component({
  selector: 'app-hotels',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatSnackBarModule],
  templateUrl: './hotels.component.html',
  styleUrl: './hotels.component.css'
})
export class HotelsComponent implements OnInit {
  filterForm: FormGroup;
  hotels       = signal<Hotel[]>([]);
  destinations = signal<Destination[]>([]);
  loading      = signal(true);

  constructor(
    private fb: FormBuilder,
    private hotelService: HotelService,
    private catalogService: CatalogService,
    private snackBar: MatSnackBar
  ) {
    this.filterForm = this.fb.group({
      destinationId: [''],
    });
  }

  ngOnInit(): void {
    this.loadDestinations();
    this.loadHotels();

    // Reload hotels when destination filter changes
    this.filterForm.get('destinationId')?.valueChanges.subscribe((id) => {
      this.loadHotels(id || undefined);
    });
  }

  loadDestinations(): void {
    this.catalogService.getDestinations().subscribe({
      next: (data) => this.destinations.set(data),
      error: () =>
        this.snackBar.open('Error loading destinations', 'Close', {
          duration: 3000,
          panelClass: ['snack-error'],
        }),
    });
  }

  loadHotels(destinationId?: number): void {
    this.loading.set(true);
    this.hotelService.getAll(destinationId).subscribe({
      next: (data) => {
        this.hotels.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
        this.snackBar.open('Error loading hotels', 'Close', {
          duration: 4000,
          panelClass: ['snack-error'],
        });
      },
    });
  }

  // Returns an array of numbers to render star icons
  getStars(count: number): number[] {
    return Array.from({ length: 5 }, (_, i) => i + 1);
  }

  selectHotel(hotel: Hotel): void {
    this.snackBar.open(`${hotel.hotelName} selected`, 'Close', {
      duration: 2000,
    });
  }
}