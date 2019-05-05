import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';
import { MessagesService } from 'src/app/services/messages/messages.service';
import { User } from '../../../models/User'
import { ApiResponse } from 'src/app/models/ApiResponse';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private messagesService: MessagesService
  ) { }

  private username: string = ''
  private name: string = ''
  private surname: string = ''
  private email: string = ''
  private password: string = ''
  private passwordRepeat: string = ''

  ngOnInit() {
  }

  clearFields(){
    this.username = ''
    this.name = ''
    this.surname = ''
    this.email = ''
    this.password = ''
    this.passwordRepeat = ''
  }

  registerUser(){
    if(this.username === '' || this.name === '' || this.surname === '' || this.email === '' || this.password === '' || this.passwordRepeat === ''){
      this.messagesService.pushMessage('Uzupełnij wszystkie pola!', 'error', 5)
      return
    }

    if(this.password != this.passwordRepeat){
      this.messagesService.pushMessage('Hasło oraz jego powtórzenie różnią się!', 'error', 5)
      return
    }

    this.authService.register(this.username, this.name, this.surname, this.email, this.password).subscribe((response: ApiResponse) => {
      if(response.error){
        this.messagesService.pushMessage(response.error, 'error', 5)
      }
      if(response.message){
        this.messagesService.pushMessage(response.message, 'successful', 5)
        this.messagesService.pushMessage('Możesz się teraz zalogować!', 'successful', 5)
        this.clearFields()
      }
    })
  }  

}
