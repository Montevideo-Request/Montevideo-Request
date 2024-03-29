import { Injectable } from '@angular/core';
import { environment } from './../../environments/environment';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Parser } from '../models/parser';
import { Format } from '../models/format';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ParserService {
  constructor(private http: HttpClient) { }
  reqHeader = new HttpHeaders({
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Credentials': 'true',
    'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS',
    'Access-Control-Allow-Headers': 'Origin, Content-Type, X-Auth-Token, Authorization' ,
    'Authorization': `${localStorage.getItem('access_token')}`,
    'Content-Type': 'application/json; charset=UTF-8'
  });

  GetFormats(): Observable<Format[]> {
    return this.http
      .get<Format[]>(`${environment.apiUrl}/parsers` , { headers: this.reqHeader })
      .pipe(
        map(res => {
          return res.map(item => {
            return item;
          });
        })
      );
  }

  GetFields(type: string): Observable<string[]> {
      return this.http
      .get<string[]>(`${environment.apiUrl}/parsers/${type}` , { headers: this.reqHeader })
      .pipe(
        map(res => {
          return res.map(item => {
            return item;
          });
        })
      );
  }

  Convert(parser: Parser): Observable<Parser> {
    return this.http
      .post<Parser>(
        `${environment.apiUrl}/parsers`,
        parser,
        { headers: this.reqHeader })
      .pipe(catchError(this.errorHandler));
  }

  private errorHandler(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
        console.error('An error occurred:', error.error);
    } else {
      if (error.status === 400 || error.status === 403) {
        return throwError(error.error);
      }
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error.Error}`
      );
    }
    return throwError('The File is empty or was not found on the server.');
  }
}
