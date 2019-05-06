import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomepageComponent } from './components/pages/homepage/homepage.component'
import { RegisterComponent } from './components/pages/register/register.component';
import { LoginComponent } from './components/pages/login/login.component';
import { AuthGuard } from './guards/auth.guard'
import { UnauthGuard } from './guards/unauth.guard'
import { DashboardComponent } from './components/pages/dashboard/dashboard.component';
import { AddWorkplacementComponent } from './components/pages/add-workplacement/add-workplacement.component';

const routes: Routes = [
  {path: '', component: HomepageComponent},
  {path: 'register', component: RegisterComponent, canActivate: [UnauthGuard]},
  {path: 'login', component: LoginComponent, canActivate: [UnauthGuard]},
  {path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard]},
  {path: 'add-workplacement', component: AddWorkplacementComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
