import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import {
  CountriesClient,
  CountryResponse,
  CountryWithoutPrefixResponse,
  PhonePrefixWithCountriesResponse,
  PrefixResponse,
  ProblemDetails,
  ApiException,
} from '../../services/CountriesClient';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Countries';
  public countries?: Observable<CountryResponse[]>;

  constructor(private client: CountriesClient) {}

  ngOnInit(): void {
    this.countries = this.client.getAll();
  }
}
