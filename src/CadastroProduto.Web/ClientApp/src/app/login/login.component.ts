import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';
import { BootstrapAlertService, BootstrapAlert } from 'ngx-bootstrap-alert';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  titulo = 'Login';
  model: any = {};

  constructor(private authService: AuthService
    , public router: Router
    , public alertService: BootstrapAlertService) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      this.router.navigate(['/home']);
    }
  }

  login() {
    this.authService.login(this.model)
      .subscribe(
        () => {
          this.router.navigate(['/home']);
          this.alertService.alert(new BootstrapAlert("Logado com Sucesso!", "alert-success"));
        },
        error => {
          this.alertService.alert(new BootstrapAlert("Falha ao tentar Logar", "alert-warning"));
        }
      );
  }

}
