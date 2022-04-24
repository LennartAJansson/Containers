import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';

import { Routes, RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CountriesApiComponent } from './countries/countries-api.component';
import { FirstComponent } from './first/first.component';
import { SecondComponent } from './second/second.component';
import { CountriesApiService } from './countries-api.service';

@NgModule({
  declarations: [
    AppComponent,
    CountriesApiComponent,
    FirstComponent,
    SecondComponent,
  ],
  imports: [BrowserModule, AppRoutingModule, BrowserAnimationsModule],
  providers: [CountriesApiService, HttpClient, HttpHandler],
  bootstrap: [AppComponent],
})
export class AppModule {}
