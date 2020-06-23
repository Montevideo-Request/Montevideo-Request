import { Injectable } from '@angular/core';
import { map, tap, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { AdditionalField } from '../models/additionalField';
import { FieldRange } from '../models/fieldRange';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdditionalFieldService {
  constructor(private http: HttpClient) { }
  reqHeader = new HttpHeaders({
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS',
    'Access-Control-Allow-Headers': 'Origin, Content-Type, X-Auth-Token, Authorization',
    'Authorization': `${localStorage.getItem('access_token')}`,
    'Content-Type': 'application/json; charset=UTF-8'
  });

  getAdditionalFields(): Observable<AdditionalField[]> {
    return this.http
      .get<AdditionalField[]>(
        `${environment.apiUrl}/additionalfields`,
        { headers: this.reqHeader })
      .pipe(
        map(res => {
          return res.map(item => {
            return item;
          });
        })
      );
  }

  getAdditionalFieldById(additionalField: AdditionalField | number): Observable<AdditionalField> {
    const id = typeof additionalField === 'number' ? additionalField : additionalField.id;
    return this.http
      .get<AdditionalField>(
        `${environment.apiUrl}/additionalfields/${id}`,
        { headers: this.reqHeader })
      .pipe(
        map(s => {
          return s;
        }),
        catchError(this.errorHandler)
      );
  }

  addAdditionalField(additionalField: AdditionalField): Observable<AdditionalField> {
    return this.http
      .post<AdditionalField>(
        `${environment.apiUrl}/additionalfields`,
        additionalField,
        { headers: this.reqHeader }
      )
      .pipe(catchError(this.errorHandler));
  }

  edit(additionalField: AdditionalField): Observable<AdditionalField> {
    const id = typeof additionalField === 'number' ? additionalField : additionalField.id;
    return this.http
      .put<AdditionalField>(
        `${environment.apiUrl}/additionalfields/${id}`,
        additionalField,
        { headers: this.reqHeader })
      .pipe(catchError(this.errorHandler));
  }

  delete(additionalField: AdditionalField | number): Observable<AdditionalField> {
    const id = typeof additionalField === 'number' ? additionalField : additionalField.id;
    return this.http
      .delete<AdditionalField>(
        `${environment.apiUrl}/additionalFields/${id}`,
        { headers: this.reqHeader })
      .pipe(catchError(this.errorHandler));
  }

  addFieldRange(
    idAdditionalField: number,
    fieldRange: FieldRange
  ): Observable<FieldRange> {
    return this.http
      .post<FieldRange>(
        `${environment.apiUrl}/additionalFields/${idAdditionalField}/fieldranges`,
        fieldRange,
        { headers: this.reqHeader }
      )
      .pipe(catchError(this.errorHandler));
  }

  private errorHandler(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      if (error.status === 400 || error.status === 403) {
        return throwError(error.error.Error);
      }
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error.Error}`
      );
    }
    return throwError('Something bad happened; please try again later.');
  }
}
