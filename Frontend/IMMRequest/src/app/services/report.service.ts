import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Request } from '../models/request';
import { Type } from '../models/type';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  constructor(private http: HttpClient) { }
  reqHeader = new HttpHeaders({
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS',
    'Access-Control-Allow-Headers': 'Origin, Content-Type, X-Auth-Token, Authorization' ,
    'Authorization': `${localStorage.getItem('access_token')}`,
    'Content-Type': 'application/json; charset=UTF-8'
  });

  generateReportA(email: string, from: string, to: string): Observable<Request[]> {
    return this.http
      .get<Request[]>(
        `${environment.apiUrl}/reports/A?email=${email}&from=${from}&to=${to}`,
        { headers: this.reqHeader })
      .pipe(
        map(res => {
          return res.map(item => {
            return item;
          });
        }));
  }

  generateReportB(from: string, to: string): Observable<string[]> {
    return this.http
      .get<string[]>(
        `${environment.apiUrl}/reports/B?from=${from}&to=${to}`,
        { headers: this.reqHeader })
      .pipe(
        map(res => {
          return res.map(item => {
            return item;
          });
        }));
  }
}
