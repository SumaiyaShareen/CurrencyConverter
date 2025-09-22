
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {
  private apiUrl = 'https://localhost:7221/api/ConversionHistory';

  constructor(private http: HttpClient) {}

  saveConversion(conversion: any): Observable<any> {
    return this.http.post(this.apiUrl, conversion);
  }

  getHistory(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}
