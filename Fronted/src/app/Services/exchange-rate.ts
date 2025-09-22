import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExchangeRateService {
  private apiUrl = 'https://localhost:7221/api/ExchangeRate';

  constructor(private http: HttpClient) {}

  getAllRates(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}
