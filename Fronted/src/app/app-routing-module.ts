import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Import your standalone components
import { ConverterComponent } from './components/converter/converter';
import { CurrenciesComponent } from './components/currencies/currencies';
import { ExchangeRateComponent } from './components/exchange-rates/exchange-rates';
import { HistoryComponent } from './components/history/history';

const routes: Routes = [
  { path: '', redirectTo: 'converter', pathMatch: 'full' }, // Default route
  { path: 'converter', component: ConverterComponent },
  { path: 'currencies', component: CurrenciesComponent },
  { path: 'exchange-rates', component: ExchangeRateComponent },
  { path: 'history', component: HistoryComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
