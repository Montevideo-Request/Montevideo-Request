import { Injectable } from '@angular/core';
import { environment } from './../../environments/environment';
import { Type } from '../models/type';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TypeService {
  constructor(private http: HttpClient) { }
  reqHeader = new HttpHeaders({
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS',
    'Access-Control-Allow-Headers': 'Origin, Content-Type, X-Auth-Token, Authorization' ,
    'Authorization': `${localStorage.getItem('access_token')}`,
    'Content-Type': 'application/json; charset=UTF-8'
  });

  getTypes(): Observable<Type[]> {
    return this.http
      .get<Type[]>(`${environment.apiUrl}/types`, { headers: this.reqHeader })
      .pipe(
        map(res => {
          return res.map(item => {
            return item;
          });
        })
      );
  }

  getById(type: Type | number): Observable<Type> {
    const id = typeof type === 'number' ? type : type.id;
    return this.http
      .get<Type>(
        `${environment.apiUrl}/types/${id}`,
        { headers: this.reqHeader })
      .pipe(
        map(s => {
          return s;
        }),
        catchError(this.handleError)
      );
  }

  delete(type: Type | number): Observable<Type> {
    const id = typeof type === 'number' ? type : type.id;
    return this.http
      .delete<Type>(
        `${environment.apiUrl}/types/${id}`,
        { headers: this.reqHeader })
      .pipe(catchError(this.handleError));
  }

  add(type: Type): Observable<Type> {
    return this.http
      .post<Type>(
        `${environment.apiUrl}/types`,
        type,
        { headers: this.reqHeader })
      .pipe(catchError(this.handleError));
  }

  edit(type: Type): Observable<Type> {
    const id = typeof type === 'number' ? type : type.id;
    return this.http
    .put<Type>(
      `${environment.apiUrl}/types/${id}`,
      type,
      { headers: this.reqHeader })
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
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