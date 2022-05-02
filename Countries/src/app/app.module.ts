import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './components/app/app.component';
import { CountriesClient, API_BASE_URL } from './services/CountriesClient';

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule],
  providers: [
    { provide: API_BASE_URL, useValue: 'https://localhost:7178' },
    CountriesClient,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
