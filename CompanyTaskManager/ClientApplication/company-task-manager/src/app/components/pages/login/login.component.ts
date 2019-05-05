import { Component, OnInit } from '@angular/core';
import { MessagesService } from 'src/app/services/messages/messages.service';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(
    private messagesService: MessagesService,
    private authService: AuthService
  ) { }

  private password: string = ''
  private username: string = ''

  ngOnInit() {
  }

  loginUser(){
    if(this.password === '' || this.username === ''){
      this.messagesService.pushMessage('Uzupełnij wszystkie pola formularza!', 'error', 5)
    }

    this.authService.login(this.username, this.password)
  }

}
