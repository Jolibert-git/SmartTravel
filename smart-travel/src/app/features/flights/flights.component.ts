import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { FlightService, CatalogService } from '../../core/services/api.services';
import { Flight, Airport } from '../../core/models/models';

@Component({
  selector: 'app-flights',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatSnackBarModule],
  templateUrl: './flights.component.html',
  styleUrl: './flights.component.css'
})
export class FlightsComponent implements OnInit {
  searchForm: FormGroup;
  flights  = signal<Flight[]>([]);
  airports = signal<Airport[]>([]);
  loading  = signal(false);
  searched = signal(false);

  constructor(
    private fb: FormBuilder,
    private flightService: FlightService,
    private catalogService: CatalogService,
    private snackBar: MatSnackBar
  ) {
    this.searchForm = this.fb.group({
      originAirportId:  ['', Validators.required],
      arrivalAirportId: ['', Validators.required],
      dateDeparture:    ['', Validators.required],
      passengers:       [1,  [Validators.required, Validators.min(1)]],
    });
  }

  ngOnInit(): void {
    this.loadAirports();
  }

  loadAirports(): void {
    this.catalogService.getAirports().subscribe({
      next: (data) => this.airports.set(data),
      error: () =>
        this.snackBar.open('Error loading airports', 'Close', {
          duration: 3000,
          panelClass: ['snack-error'],
        }),
    });
  }

  search(): void {
    if (this.searchForm.invalid) return;
    this.loading.set(true);
    this.searched.set(true);

    this.flightService.search(this.searchForm.value).subscribe({
      next: (data) => {
        this.flights.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
        this.snackBar.open('Error searching flights', 'Close', {
          duration: 4000,
          panelClass: ['snack-error'],
        });
      },
    });
  }

  // Calculate flight duration in hours and minutes
  getDuration(departure: string, arrival: string): string {
    const diff = new Date(arrival).getTime() - new Date(departure).getTime();
    const hours   = Math.floor(diff / 3600000);
    const minutes = Math.floor((diff % 3600000) / 60000);
    return `${hours}h ${minutes}m`;
  }

  selectFlight(flight: Flight): void {
    // Navigation to booking will be implemented later
    this.snackBar.open(`Flight #${flight.idFlight} selected`, 'Close', {
      duration: 2000,
    });
  }
}