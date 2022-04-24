import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CountriesApiComponent } from './countries-api.component';

describe('CountriesApiComponent', () => {
  let component: CountriesApiComponent;
  let fixture: ComponentFixture<CountriesApiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CountriesApiComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CountriesApiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
