import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms' 
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './interceptors/jwt.interceptor'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomepageComponent } from './components/pages/homepage/homepage.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { RegisterComponent } from './components/pages/register/register.component';
import { MessagesComponent } from './components/messages/messages.component';
import { LoginComponent } from './components/pages/login/login.component';
import { DashboardComponent } from './components/pages/dashboard/dashboard.component';
import { AddWorkplacementComponent } from './components/pages/add-workplacement/add-workplacement.component';
import { WorkplacementComponent } from './components/pages/workplacement/workplacement.component';

@NgModule({
  declarations: [
    AppComponent,
    HomepageComponent,
    NavbarComponent,
    RegisterComponent,
    MessagesComponent,
    LoginComponent,
    DashboardComponent,
    AddWorkplacementComponent,
    WorkplacementComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
