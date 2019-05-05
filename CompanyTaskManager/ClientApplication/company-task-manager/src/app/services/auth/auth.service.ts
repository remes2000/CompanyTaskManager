import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { ApiResponse } from 'src/app/models/ApiResponse';
import { Observable, BehaviorSubject } from 'rxjs';
import { User } from 'src/app/models/User';
import { Router } from '@angular/router';
import { MessagesService } from '../messages/messages.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<User>
  public currentUser: Observable<User>

  constructor(
    private apiService: ApiService,
    private router: Router,
    private messagesService: MessagesService
  ) { 
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('companyTaskManagerCurrentUser')))
    this.currentUser = this.currentUserSubject.asObservable()
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value
  }

  register(username: string, name: string, surname: string, email: string, password: string):  Observable<ApiResponse>{
    return this.apiService.post('/Users', {username, name, surname, email, password})
  }

  login(username: string, password: string){
    this.apiService.post('/Users/authenticate', {username, password}).subscribe(
      (response: any) => {

        if(response.error){
          return this.messagesService.pushMessage(response.error, 'error', 5)
        }

        const user = response as User
        
        if(user && user.token) {
          localStorage.setItem('companyTaskManagerCurrentUser', JSON.stringify(user))
          this.currentUserSubject.next(user)
        }

        this.router.navigate(['/'])
        this.messagesService.pushMessage('Zostałeś pomyślnie zalogowany!', 'successful', 3)
        return user
      }
    )
  }

  logout(){
    localStorage.removeItem('companyTaskManagerCurrentUser')
    this.currentUserSubject.next(null)
    this.router.navigate(['/login'])
  }

  isUserLoggedIn(){
    const currentUser = this.currentUserValue
    if (currentUser) {
        return true
    }
    return false
  }
}
