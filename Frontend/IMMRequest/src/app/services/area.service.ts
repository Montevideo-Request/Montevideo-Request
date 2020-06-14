import { Injectable } from '@angular/core';
import { environment } from './../../environments/environment';
import { Area } from '../models/area';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AreaService {
  constructor(private http: HttpClient) { }
  reqHeader = new HttpHeaders({
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS',
    'Access-Control-Allow-Headers': 'Origin, Content-Type, X-Auth-Token, Authorization',
    Authorization: `Bearer ${localStorage.getItem('access_token')}`,
    'Content-Type': 'application/json; charset=UTF-8'
  });

  getAreas(): Observable<Area[]> {
    return this.http
      .get<Area[]>(
        `${environment.apiUrl}/areas`,
        { headers: this.reqHeader })
      .pipe(
        map(res => {
          return res.map(item => {
            return item;
          });
        })
      );
  }

  getById(area: Area | number): Observable<Area> {
    const id = typeof area === 'number' ? area : area.Id;
    return this.http
      .get<Area>(
        `${environment.apiUrl}/areas/${id}`,
        { headers: this.reqHeader })
      .pipe(
        map(s => {
          return s;
        }),
        catchError(this.handleError)
      );
  }

  delete(area: Area | number): Observable<Area> {
    const id = typeof area === 'number' ? area : area.Id;
    return this.http
      .delete<Area>(
        `${environment.apiUrl}/areas/${id}`,
        { headers: this.reqHeader })
      .pipe(catchError(this.handleError));
  }

  add(area: Area): Observable<Area> {
    return this.http
      .post<Area>(
        `${environment.apiUrl}/areas`,
        area,
        { headers: this.reqHeader })
      .pipe(catchError(this.handleError));
  }

  edit(area: Area): Observable<Area> {
    const id = typeof area === 'number' ? area : area.Id;
    return this.http
      .put<Area>(
        `${environment.apiUrl}/areas/${id}`,
        area,
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
