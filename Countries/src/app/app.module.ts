import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './components/app/app.component';
import { CountriesClient, API_BASE_URL } from './services/CountriesClient';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, BrowserAnimationsModule],
  providers: [
    { provide: API_BASE_URL, useValue: environment.url },
    CountriesClient,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
