import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomepageComponent } from './components/pages/homepage/homepage.component'
import { RegisterComponent } from './components/pages/register/register.component';
import { LoginComponent } from './components/pages/login/login.component';
import { AuthGuard } from './guards/auth.guard'
import { UnauthGuard } from './guards/unauth.guard'
import { DashboardComponent } from './components/pages/dashboard/dashboard.component';
import { AddWorkplacementComponent } from './components/pages/add-workplacement/add-workplacement.component';
import { WorkplacementComponent } from './components/pages/workplacement/workplacement.component';
import { AddEmployeeComponent } from './components/pages/workplacement/modals/add-employee/add-employee.component';
import { AddTaskComponent } from './components/pages/workplacement/modals/add-task/add-task.component';
import { DisplayTaskComponent } from './components/pages/workplacement/modals/display-task/display-task.component';
import { DeleteEmployeeComponent } from './components/pages/workplacement/modals/delete-employee/delete-employee.component';
import { DeleteWorkplacementComponent } from './components/pages/dashboard/modals/delete-workplacement/delete-workplacement.component';

const routes: Routes = [
  {path: '', component: HomepageComponent},
  {path: 'register', component: RegisterComponent, canActivate: [UnauthGuard]},
  {path: 'login', component: LoginComponent, canActivate: [UnauthGuard]},
  {path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard]},
  {path: 'add-workplacement', component: AddWorkplacementComponent, canActivate: [AuthGuard]},
  {path: 'workplacement/:id', component: WorkplacementComponent, canActivate: [AuthGuard]},
  {path: 'add-employee/:workplacementId', component: AddEmployeeComponent, outlet: 'modal', canActivate: [AuthGuard]},
  {path: 'add-task/:workplacementId/:userId', component: AddTaskComponent, outlet: 'modal', canActivate: [AuthGuard]},
  {path: 'display-task', component: DisplayTaskComponent, outlet: 'modal', canActivate: [AuthGuard]},
  {path: 'delete-employee/:workplacementId', component: DeleteEmployeeComponent, outlet: 'modal', canActivate: [AuthGuard]},
  {path: 'delete-workplacement', component: DeleteWorkplacementComponent, outlet: 'modal', canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
