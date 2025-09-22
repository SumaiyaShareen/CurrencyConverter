import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';

// Non-standalone Components
import { Navbar } from './components/navbar/navbar';
import { Sidebar } from './components/sidebar/sidebar';

// Standalone Components
import { ConverterComponent } from './components/converter/converter';
import { CurrenciesComponent} from './components/currencies/currencies';
import { ExchangeRateComponent } from './components/exchange-rates/exchange-rates';
import { HistoryComponent } from './components/history/history';

@NgModule({
  declarations: [
    App,
    Navbar,
    Sidebar // ✅ Only non-standalone components here
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,

    // ✅ Standalone components here
    ConverterComponent,
    CurrenciesComponent,
    ExchangeRateComponent,
    HistoryComponent
  ],
  providers: [],
  bootstrap: [App]
})
export class AppModule {}
