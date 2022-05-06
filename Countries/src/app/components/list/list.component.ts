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
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css'],
})
export class ListComponent implements OnInit {
  public countriesObservable?: Observable<CountryResponse[]>;
  public countries?: CountryResponse[];

  constructor(private client: CountriesClient) {}

  ngOnInit(): void {
    this.countriesObservable = this.client.getAll();
    this.countriesObservable.pipe().subscribe((obj) => {
      return (this.countries = obj);
    });
  }
}
