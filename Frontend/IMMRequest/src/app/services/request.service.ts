import { Injectable } from '@angular/core';
import { Request } from '../models/request';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { map, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class RequestService {
    constructor(private http: HttpClient) { }
    reqHeader = new HttpHeaders({
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS',
        'Access-Control-Allow-Headers': 'Origin, Content-Type, X-Auth-Token, Authorization',
        'Authorization': `${localStorage.getItem('access_token')}`,
        'Content-Type': 'application/json; charset=UTF-8'
    });

    getRequests(): Observable<Request[]> {
        return this.http
            .get<Request[]>(
                `${environment.apiUrl}/requests`,
                { headers: this.reqHeader })
            .pipe(
                map(res => {
                    return res.map(item => {
                        return item;
                    });
                })
            );
    }

    GetStates(): Observable<string[]> {
        return this.http
            .get<string[]>(
                `${environment.apiUrl}/requests/states`,
                { headers: this.reqHeader })
            .pipe(
                map(s => {
                    return s;
                }), catchError(this.handleError)
            );
    }

    getById(id: string): Observable<Request> {
        return this.http
            .get<Request>(
                `${environment.apiUrl}/requests/${id}`,
                { headers: this.reqHeader })
            .pipe(
                map(s => {
                    return s;
                }),
                catchError(this.handleError)
            );
    }

    add(request: {}): Observable<{}> {
        return this.http
            .post<Request>(
                `${environment.apiUrl}/requests`,
                request,
                { headers: this.reqHeader })
            .pipe(catchError(this.handleError));
    }

    edit(request: Request, id: string): Observable<Request> {
        return this.http
            .put<Request>(
                `${environment.apiUrl}/requests/${id}`,
                request,
                { headers: this.reqHeader })
            .pipe(catchError(this.handleError));
    }

    private handleError(error: HttpErrorResponse) {
        if (error.error instanceof ErrorEvent) {
            console.error('An error occurred:', error.message);
        } else {
            if (error.status === 400 || error.status === 403) {
                return throwError(error.error);
            }
            console.error(
                `Backend returned code ${error.status}, ` +
                `body was: ${error.error.Error}`
            );
        }
        return throwError('Something bad happened; please try again later.');
    }

}
