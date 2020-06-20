import { Injectable } from '@angular/core';
import { environment } from './../../environments/environment';
import { Topic } from '../models/topic';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class TopicService {
  constructor(private http: HttpClient) { }
  reqHeader = new HttpHeaders({
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS',
    'Access-Control-Allow-Headers':
      'Origin, Content-Type, X-Auth-Token, Authorization',
    // Authorization: `Bearer ${localStorage.getItem('access_token')}`,
    'Content-Type': 'application/json; charset=UTF-8'
  });

  getTopics(): Observable<Topic[]> {
    return this.http
      .get<Topic[]>(`${environment.apiUrl}/topics`, { headers: this.reqHeader })
      .pipe(
        map(res => {
          return res.map(item => {
            return item;
          });
        })
      );
  }

  getById(topic: Topic | number): Observable<Topic> {
    const id = typeof topic === 'number' ? topic : topic.Id;
    return this.http
      .get<Topic>(
        `${environment.apiUrl}/topics/${id}`,
        { headers: this.reqHeader })
      .pipe(
        map(s => {
          return s;
        }),
        catchError(this.handleError)
      );
  }

  delete(topic: Topic | number): Observable<Topic> {
    const id = typeof topic === 'number' ? topic : topic.Id;
    return this.http
      .delete<Topic>(
        `${environment.apiUrl}/topics/${id}`,
        { headers: this.reqHeader })
      .pipe(catchError(this.handleError));
  }

  add(topic: Topic): Observable<Topic> {
    return this.http
      .post<Topic>(`${environment.apiUrl}/topics`,
        topic,
        { headers: this.reqHeader })
      .pipe(catchError(this.handleError));
  }

  edit(topic: Topic): Observable<Topic> {
    const id = typeof topic === 'number' ? topic : topic.Id;
    return this.http
      .put<Topic>(
        `${environment.apiUrl}/topics/${id}`,
        topic,
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
