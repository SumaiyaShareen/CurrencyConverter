import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CurrenciesService } from '../../services/currency';

@Component({
  selector: 'app-currencies',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './currencies.html',
  styleUrls: ['./currencies.css']
})
export class CurrenciesComponent implements OnInit {
  currencies: any[] = [];
  loading = true;

  constructor(private currencyService: CurrenciesService) {}

  ngOnInit(): void {
    this.currencyService.getCurrencies().subscribe({
      next: (res) => {
        this.currencies = res;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching currencies', err);
        this.loading = false;
      }
    });
  }
}
