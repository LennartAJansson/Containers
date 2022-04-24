import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class CountriesApiService {
  constructor(private http: HttpClient) {}

  getCountries() {
    return this.http.get('http://countriesapi.local:8081/api/Countries/GetAll');
  }

  getCountry(id: string) {
    return this.http.get(
      'http://countriesapi.local:8081/api/Countries/Get/' + id
    );
  }
}
