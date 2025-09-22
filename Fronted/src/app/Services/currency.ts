import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Currency {
  currencyCode: string;
  currencyName: string;
  symbol: string;
  isActive: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class CurrenciesService {
  private apiUrl = 'https://localhost:7221/api/Currency'; // change as per backend

  constructor(private http: HttpClient) {}

  // Fetch all active currencies
  getCurrencies(): Observable<Currency[]> {
    return this.http.get<Currency[]>(this.apiUrl);
  }
}
