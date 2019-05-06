import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { MessagesService } from '../messages/messages.service';
import { ApiResponse } from 'src/app/models/ApiResponse';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class WorkplacementService {

  constructor(
    private apiService: ApiService,
    private messageService: MessagesService,
    private router: Router
  ) { }

  create(title: string, description: string){
    this.apiService.post('/workplacements', {title, description}).subscribe((response: ApiResponse) => {
      this.messageService.pushMessage(response.message, 'successful', 5)
      this.router.navigate([`/workplacement/${response.id}`])
    })
  }
}
