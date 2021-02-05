import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseURL = 'https://dev.sitemercado.com.br/api/login';

  constructor(private http: HttpClient
    , private toastrService: ToastrService) { }

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
          else this.toastrService.error('Usuário ou Senha incorretos');
        })
      );
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return token && token != '';
  }

}
