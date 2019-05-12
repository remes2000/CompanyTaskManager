import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(
    public authService: AuthService
  ) { }

  ngOnInit() {
  }

  hamburgerClicked(){
    const nav = document.getElementById('companytaskmanagernavigation')
    const hamburger = document.getElementById('hamburger')
    nav.classList.toggle('is-active')
    hamburger.classList.toggle('is-active')
  }
}
