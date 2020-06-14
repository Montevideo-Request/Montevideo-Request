import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Log } from '../models/log';
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
    'Access-Control-Allow-Headers': 'Origin, Content-Type, X-Auth-Token, Authorization',
    Authorization: `Bearer ${localStorage.getItem('access_token')}`,
    'Content-Type': 'application/json; charset=UTF-8'
  });

  // creo que recibe un mail tambien
  getLogs(from: string, to: string): Observable<Log[]> {
    return this.http
      .get<Log[]>(
        `${environment.apiUrl}/report/logs?from=${from}&to=${to}`,
        { headers: this.reqHeader })
      .pipe(
        map(res => {
          return res.map(item => {
            return item;
          });
        }));
  }
}
