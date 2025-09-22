import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExchangeRateService } from '../../services/exchange-rate';

@Component({
  selector: 'app-exchange-rate',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './exchange-rates.html',
  styleUrls: ['./exchange-rates.css']
})
export class ExchangeRateComponent implements OnInit {
  rates: any[] = [];
  loading = true;

  constructor(private exchangeRateService: ExchangeRateService) {}

  ngOnInit(): void {
    this.exchangeRateService.getAllRates().subscribe({
      next: (res) => {
        this.rates = res;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching exchange rates', err);
        this.loading = false;
      }
    });
  }
}
