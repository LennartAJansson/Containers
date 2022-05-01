import { Component } from '@angular/core';
import {
  CountriesClient,
  Country,
  Prefix,
} from '../../services/CountriesClient';

@Component({
  selector: 'app-countries',
  templateUrl: './Countries.component.html',
})
export class CountriesComponent {
  public countries: Country[] = [];

  constructor(sampleDataService: CountriesClient) {
    sampleDataService
      .getAll()
      .toPromise()
      .then((result) => {
        this.countries = result as unknown as Country[];
      });
  }
}
