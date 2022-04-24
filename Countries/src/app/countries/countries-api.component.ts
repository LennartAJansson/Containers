import { Component, OnInit } from '@angular/core';
import { CountriesApiService } from '../countries-api.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'countries',
  templateUrl: './countries-api.component.html',
  styleUrls: ['./countries-api.component.scss'],
  providers: [CountriesApiService],
})
export class CountriesApiComponent implements OnInit {
  countries: Object;

  constructor(private api: CountriesApiService) {
    this.countries = new Object();
  }

  ngOnInit(): void {
    // this.api.getCountries().subscribe((res) => {
    //   this.countries = res;
    console.log('read countries from API');
    // });
  }
}
