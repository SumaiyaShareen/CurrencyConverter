import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CurrenciesService } from '../../services/currency';
import { ExchangeRateService } from '../../services/exchange-rate';
import { HistoryService } from '../../services/history';

@Component({
  selector: 'app-converter',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './converter.html',
  styleUrls: ['./converter.css']
})
export class ConverterComponent implements OnInit {
  currencies: any[] = [];
  fromCurrency: string = 'USD';
  toCurrency: string = 'PKR';
  amount: number = 1;
  convertedAmount: number | null = null;
  rateUsed: number | null = null;
  error: string = '';
  loading: boolean = false;
  history: any[] = [];

  constructor(
    private currencyService: CurrenciesService,
    private exchangeRateService: ExchangeRateService,
    private historyService: HistoryService
  ) {}

  ngOnInit(): void {
    // Load currencies
    this.currencyService.getCurrencies().subscribe({
      next: (res) => this.currencies = res,
      error: () => this.error = 'Failed to load currencies'
    });

    // Load history initially
    this.loadHistory();
  }

  convert(): void {
    if (!this.amount || this.amount <= 0) {
      this.error = '⚠️ Please enter a valid amount';
      return;
    }

    this.loading = true;
    this.exchangeRateService.getAllRates().subscribe({
      next: (rates) => {
        const match = rates.find((r: any) =>
          r.baseCurrency.toUpperCase() === this.fromCurrency.toUpperCase() &&
          r.targetCurrency.toUpperCase() === this.toCurrency.toUpperCase()
        );

        if (match) {
          this.rateUsed = match.rate;
          this.convertedAmount = this.amount * match.rate;
          this.error = '';

          // Save conversion to history
          this.historyService.saveConversion({
            userId: 0, // replace with logged-in user id if available
            fromCurrency: this.fromCurrency,
            toCurrency: this.toCurrency,
            amount: this.amount,
            convertedAmount: this.convertedAmount,
            rateUsed: this.rateUsed
          }).subscribe({
            next: () => {
              this.loadHistory(); // reload history after conversion
            },
            error: (err) => {
              console.error('Error saving history', err);
              this.error = '❌ Failed to save conversion to history';
            }
          });

        } else {
          this.convertedAmount = null;
          this.error = `❌ No rate found for ${this.fromCurrency} → ${this.toCurrency}`;
        }
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching exchange rates', err);
        this.error = '🚫 Could not fetch rates. Try again later.';
        this.loading = false;
      }
    });
  }

  loadHistory(): void {
    this.historyService.getHistory().subscribe({
      next: (res) => this.history = res,
      error: (err) => console.error('Error fetching history', err)
    });
  }
}
