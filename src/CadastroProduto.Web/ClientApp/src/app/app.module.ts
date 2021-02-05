import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthInterceptor } from './auth/auth.interceptor';
import { ProdutoService } from './_services/produto.service';
import { AppRoutingModule } from './app.routing.module';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginComponent } from './login/login.component';
import { ProdutosComponent } from './produtos/produtos.component';
import { ProdutoEditComponent } from './produtos/produtoEdit/produtoEdit.component';
import { NgxMaskModule } from 'ngx-mask';
import { NgxCurrencyModule } from 'ngx-currency';
import { TituloComponent } from './_shared/titulo/titulo.component';
import { BootstrapAlertModule } from 'ngx-bootstrap-alert';

@NgModule({
  declarations: [
    AppComponent,
    TituloComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    ProdutosComponent,
    ProdutoEditComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    NgxMaskModule.forRoot(),
    NgxCurrencyModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    BootstrapAlertModule,
  ],
  providers: [
    ProdutoService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
