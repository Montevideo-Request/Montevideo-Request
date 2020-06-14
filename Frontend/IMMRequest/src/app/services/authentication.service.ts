import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient) { }
  
  login(Email: string, Password: string) {
    const reqHeader = new HttpHeaders({'Access-Control-Allow-Origin': '*' ,
    'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS' ,
    'Access-Control-Allow-Headers': 'Origin, Content-Type, X-Auth-Token'});
    return this.http
        .post<any>(
          `${environment.apiUrl}/sessions`,
          {Email, Password}, {headers: reqHeader})
        .pipe(map(admin => {
          if (admin && admin.Token) {
            localStorage.setItem('access_token', admin.Token);
          }
          return admin;
        }));
  }

  logout() {
    localStorage.removeItem('access_token');
  }

  public get isLoggedIn(): boolean {
    return (localStorage.getItem('access_token') != null);
  }

}
