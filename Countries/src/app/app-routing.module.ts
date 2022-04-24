import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CountriesApiComponent } from './countries/countries-api.component';
import { FirstComponent } from './first/first.component';
import { SecondComponent } from './second/second.component';

const routes: Routes = [
  { path: 'countries', component: CountriesApiComponent },
  { path: 'first-component', component: FirstComponent },
  { path: 'second-component', component: SecondComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
