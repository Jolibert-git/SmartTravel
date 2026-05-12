import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { PaymentService, CatalogService, ReservationService } from '../../core/services/api.services';
import { Payment, PaymentMethod, Reservation } from '../../core/models/models';

@Component({
  selector: 'app-payments',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatSnackBarModule],
  templateUrl: './payments.component.html',
  styleUrl: './payments.component.css'
})
export class PaymentsComponent implements OnInit {
  payments        = signal<Payment[]>([]);
  paymentMethods  = signal<PaymentMethod[]>([]);
  reservations    = signal<Reservation[]>([]);
  loading         = signal(true);
  showForm        = signal(false);
  saving          = signal(false);

  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private paymentService: PaymentService,
    private reservationService: ReservationService,
    private catalogService: CatalogService,
    private snackBar: MatSnackBar
  ) {
    this.form = this.fb.group({
      idReservation:   ['', Validators.required],
      amount:          ['', [Validators.required, Validators.min(0.01)]],
      idPaymentMethod: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.loadAll();
  }

  loadAll(): void {
    this.loading.set(true);

    this.paymentService.getAll().subscribe({
      next: (data) => {
        this.payments.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
        this.snackBar.open('Error loading payments', 'Close', {
          duration: 4000,
          panelClass: ['snack-error'],
        });
      },
    });

    this.catalogService.getPaymentMethods().subscribe({
      next: (data) => this.paymentMethods.set(data.filter((m) => m.isActive)),
    });

    this.reservationService.getMyReservations().subscribe({
      next: (data) => this.reservations.set(data),
    });
  }

  toggleForm(): void {
    this.showForm.set(!this.showForm());
    if (!this.showForm()) this.form.reset();
  }

  save(): void {
    if (this.form.invalid) return;
    this.saving.set(true);

    this.paymentService.create(this.form.value).subscribe({
      next: () => {
        this.snackBar.open('Payment registered successfully', 'Close', {
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
        this.snackBar.open('Error processing payment', 'Close', {
          duration: 4000,
          panelClass: ['snack-error'],
        });
      },
    });
  }

  getStatusClass(status: string | undefined): string {
    switch (status?.toLowerCase()) {
      case 'completed': return 'badge-completed';
      case 'pending':   return 'badge-pending';
      case 'failed':    return 'badge-failed';
      case 'refunded':  return 'badge-refunded';
      default:          return 'badge-pending';
    }
  }

  getError(field: string): string {
    const ctrl = this.form.get(field);
    if (!ctrl?.touched) return '';
    if (ctrl.hasError('required')) return 'This field is required';
    if (ctrl.hasError('min'))      return 'Amount must be greater than 0';
    return '';
  }

  // Calculate total paid amount
  get totalPaid(): number {
    return this.payments()
      .filter((p) => p.statusDescription?.toLowerCase() === 'completed')
      .reduce((sum, p) => sum + p.amount, 0);
  }
}