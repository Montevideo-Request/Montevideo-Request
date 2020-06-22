import { Injectable, EventEmitter, Output } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from './../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {

    @Output() fireLogin: EventEmitter<any> = new EventEmitter();
    @Output() fireLogout: EventEmitter<any> = new EventEmitter();

    constructor(private http: HttpClient) { }

    login(Email: string, Password: string) {
        const reqHeader = new HttpHeaders({
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS',
            'Access-Control-Allow-Headers': 'Origin, Content-Type, X-Auth-Token',
            'Content-Type': 'application/json; charset=UTF-8'
        });
        return this.http
            .post<any>(
                `${environment.apiUrl}/login`,
                { Email, Password }, { headers: reqHeader })
            .pipe(map(token => {
                if (token) {
                    localStorage.setItem('access_token', token);
                    this.fireLogin.emit(token);
                }
                return token;
            }));
    }

    logout() {
        localStorage.removeItem('access_token');
        this.fireLogout.emit();
    }

    public get isLoggedIn(): boolean {
        return (localStorage.getItem('access_token') != null);
    }

    getLoginEmitter() {
        return this.fireLogin;
    }

    getLogOutEmitter() {
        return this.fireLogout;
    }

}
