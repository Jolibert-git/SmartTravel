import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { PackageService } from '../../core/services/api.services';
import { Package } from '../../core/models/models';


@Component({
  selector: 'app-packages',
  standalone: true,
  imports: [CommonModule, MatSnackBarModule],
  templateUrl: './packages.component.html',
  styleUrl: './packages.component.css'
})

export class PackagesComponent implements OnInit {
  packages = signal<Package[]>([]);
  loading  = signal(true);

  constructor(
    private packageService: PackageService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadPackages();
  }

  loadPackages(): void {
    this.loading.set(true);
    this.packageService.getAll().subscribe({
      next: (data) => {
        this.packages.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
        this.snackBar.open('Error loading packages', 'Close', {
          duration: 4000,
          panelClass: ['snack-error'],
        });
      },
    });
  }

  // Check if the package offer is still active
  isActive(offerEnd: string): boolean {
    return new Date(offerEnd) >= new Date();
  }

  // Calculate days remaining until offer ends
  daysRemaining(offerEnd: string): number {
    const diff = new Date(offerEnd).getTime() - new Date().getTime();
    return Math.max(0, Math.ceil(diff / (1000 * 60 * 60 * 24)));
  }

  selectPackage(pkg: Package): void {
    this.snackBar.open(`Package "${pkg.packageName}" selected`, 'Close', {
      duration: 2000,
    });
  }
}