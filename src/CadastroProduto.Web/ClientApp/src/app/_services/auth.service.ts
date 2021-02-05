import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseURL = 'https://dev.sitemercado.com.br/api/login';

  constructor(private http: HttpClient) { }

  login(model: any) {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Basic ' + btoa(`${model.username}:${model.password}`)
      })
    };

    return this.http
      .post(this.baseURL, model, httpOptions).pipe(
        map((response: any) => {
          if (response && response.success) {
            localStorage.setItem('token', 'token ficticio');
            sessionStorage.setItem('username', 'Josué Monteiro');
          }
          else $.notify({ message: 'Usuário ou Senha incorretos' },{ type: 'danger' });
        })
      );
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return token && token != '';
  }

}
