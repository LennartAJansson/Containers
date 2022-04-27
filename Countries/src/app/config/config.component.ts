import { Component } from '@angular/core';
import { Country, Prefix, CountriesService } from './countries.service';

@Component({
  selector: 'app-config',
  templateUrl: './config.component.html',
  providers: [ CountriesService ],
  styles: ['.error { color: #b30000; }']
})

export class ConfigComponent {
  error: any;
  headers: string[] = [];
  config: Country | undefined;

  constructor(private countriesService: CountriesService) {}

  showConfig() {
    this.countriesService.getConfig()
      .subscribe({
        next: (data: Country) => this.config = { ...data },
        error: error => this.error = error, 
      });
  }

  showConfigResponse() {
    this.countriesService.getConfigResponse()
      .subscribe(resp => {
        const keys = resp.headers.keys();
        this.headers = keys.map(key =>
          `${key}: ${resp.headers.get(key)}`);
        this.config = { ...resp.body! };
      });
  }
}
