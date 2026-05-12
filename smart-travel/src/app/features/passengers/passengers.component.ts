import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { PassengerService, CatalogService } from '../../core/services/api.services';
import { Passenger, Country, DocumentType } from '../../core/models/models';

@Component({
  selector: 'app-passengers',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatSnackBarModule],
  templateUrl: './passengers.component.html',
  styleUrl: './passengers.component.css'
})
export class PassengersComponent implements OnInit {
  passengers     = signal<Passenger[]>([]);
  countries      = signal<Country[]>([]);
  documentTypes  = signal<DocumentType[]>([]);
  loading        = signal(true);
  showForm       = signal(false);
  saving         = signal(false);

  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private passengerService: PassengerService,
    private catalogService: CatalogService,
    private snackBar: MatSnackBar
  ) {
    this.form = this.fb.group({
      firstName:      ['', [Validators.required, Validators.minLength(2)]],
      lastName:       ['', [Validators.required, Validators.minLength(2)]],
      birthDate:      ['', Validators.required],
      documentNumber: ['', Validators.required],
      idDocumentType: ['', Validators.required],
      idCountry:      ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.loadAll();
  }

  loadAll(): void {
    this.loading.set(true);
    this.passengerService.getAll().subscribe({
      next: (data) => {
        this.passengers.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
        this.snackBar.open('Error loading passengers', 'Close', {
          duration: 4000,
          panelClass: ['snack-error'],
        });
      },
    });

    this.catalogService.getCountries().subscribe({
      next: (data) => this.countries.set(data),
    });

    this.catalogService.getDocumentTypes().subscribe({
      next: (data) => this.documentTypes.set(data),
    });
  }

  toggleForm(): void {
    this.showForm.set(!this.showForm());
    if (!this.showForm()) this.form.reset();
  }

  save(): void {
    if (this.form.invalid) return;
    this.saving.set(true);

    this.passengerService.create(this.form.value).subscribe({
      next: () => {
        this.snackBar.open('Passenger added successfully', 'Close', {
          duration: 3000,
          panelClass: ['snack-success'],
        });
        this.saving.set(false);
        this.showForm.set(false);
        this.form.reset();
        this.loadAll();
      },
      error: () => {
        this.saving.set(false);
        this.snackBar.open('Error saving passenger', 'Close', {
          duration: 4000,
          panelClass: ['snack-error'],
        });
      },
    });
  }

  delete(id: number): void {
    this.passengerService.delete(id).subscribe({
      next: () => {
        this.snackBar.open('Passenger removed', 'Close', { duration: 3000 });
        this.loadAll();
      },
      error: () =>
        this.snackBar.open('Error removing passenger', 'Close', {
          duration: 4000,
          panelClass: ['snack-error'],
        }),
    });
  }

  getError(field: string): string {
    const ctrl = this.form.get(field);
    if (!ctrl?.touched) return '';
    if (ctrl.hasError('required'))  return 'This field is required';
    if (ctrl.hasError('minlength')) return `Minimum ${ctrl.getError('minlength').requiredLength} characters`;
    return '';
  }
}