import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

//https://angular.io/guide/http

export interface Country {
    isoCountry: string;
    countryCode2: string;
    countryCode3: string;
    countryName: string;
    phonePrefixes: Prefix[];
}

export interface Prefix {
    prefix: string;
}

@Injectable()
export class CountriesService {
    configUrl = 'http://countriesapi.local:8081/Countries/GetByIso/752';

    constructor(private http: HttpClient) { }

    getConfig() {
        return this.http.get<Country>(this.configUrl)
          .pipe(
            retry(3), // retry a failed request up to 3 times
            catchError(this.handleError) // then handle the error
          );
    }

    getConfigResponse(): Observable<HttpResponse<Country>> {
        return this.http.get<Country>(
          this.configUrl, { observe: 'response' });
    }
    
    private handleError(error: HttpErrorResponse) {
        if (error.status === 0) {
          // A client-side or network error occurred. Handle it accordingly.
          console.error('An error occurred:', error.error);
        } else {
          // The backend returned an unsuccessful response code.
          // The response body may contain clues as to what went wrong.
          console.error(
            `Backend returned code ${error.status}, body was: `, error.error);
        }
        // Return an observable with a user-facing error message.
        return throwError(() => new Error('Something bad happened; please try again later.'));
    }
}