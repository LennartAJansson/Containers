import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { ListComponent } from './components/list/list.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { BuildVersionsClient, API_BASE_URL } from './services/BuildVersionsClient';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [AppComponent, NavMenuComponent, HomeComponent, ListComponent],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, BrowserAnimationsModule],
  providers: [
    { provide: API_BASE_URL, useValue: environment.url },
    BuildVersionsClient,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
