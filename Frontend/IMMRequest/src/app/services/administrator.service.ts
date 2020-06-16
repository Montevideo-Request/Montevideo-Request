import { Injectable } from '@angular/core';
import { environment } from './../../environments/environment';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Administrator } from '../models/administrator';
import { AdministratorBasicInfo } from '../models/administratorBasicInfo';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AdministratorService {
  constructor(private http: HttpClient) { }
  reqHeader = new HttpHeaders({
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Credentials': 'true',
    'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS',
    'Access-Control-Allow-Headers': 'Origin, Content-Type, X-Auth-Token',
    // Authorization: `Bearer ${localStorage.getItem('access_token')}`,
    'Content-Type': 'application/json; charset=UTF-8'
  });

  GetAll(): Observable<AdministratorBasicInfo[]> {
    return this.http
      .get<AdministratorBasicInfo[]>(
        `${environment.apiUrl}/administrators`)
      .pipe(
        catchError(this.errorHandler
        )
      );
  }

  getById(administrator: Administrator | number): Observable<Administrator> {
    const id = typeof administrator === 'number' ? administrator : administrator.Id;
    return this.http
      .get<Administrator>(
        `${environment.apiUrl}/administrators/${id}`,
        { headers: this.reqHeader })
      .pipe(
        map(user => {
          if (user) {
            localStorage.setItem('id', JSON.stringify(user.Id));
          }
          return user;
        })
        , catchError(this.errorHandler));
  }

  delete(administrator: AdministratorBasicInfo | number): Observable<AdministratorBasicInfo> {
    const id = typeof administrator === 'number' ? administrator : administrator.id;
    return this.http
      .delete<AdministratorBasicInfo>(
        `${environment.apiUrl}/administrators/${id}`,
        { headers: this.reqHeader })
      .pipe(catchError(this.errorHandler));
  }

  add(administrator: Administrator): Observable<Administrator> {
    return this.http
      .post<Administrator>(
        `${environment.apiUrl}/administrators`,
        administrator,
        { headers: this.reqHeader })
      .pipe(catchError(this.errorHandler));
  }

  edit(administrator: AdministratorBasicInfo): Observable<AdministratorBasicInfo> {
    const id = typeof administrator === 'number' ? administrator : administrator.id;
    return this.http
      .put<AdministratorBasicInfo>(
        `${environment.apiUrl}/administrators/${id}`,
        administrator,
        { headers: this.reqHeader })
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
