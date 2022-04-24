import { TestBed } from '@angular/core/testing';

import { CountriesApiService } from './countries-api.service';

describe('CountriesApiService', () => {
  let service: CountriesApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CountriesApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
