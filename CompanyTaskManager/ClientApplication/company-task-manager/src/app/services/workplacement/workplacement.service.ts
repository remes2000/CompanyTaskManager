import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { MessagesService } from '../messages/messages.service';
import { ApiResponse } from 'src/app/models/ApiResponse';
import { Router } from '@angular/router';
import { User } from 'src/app/models/User';

@Injectable({
  providedIn: 'root'
})
export class WorkplacementService {

  constructor(
    private apiService: ApiService,
    private messageService: MessagesService,
    private router: Router
  ) { }

  public currentWorkplacementsMembers: User[] = []
  public selectedUser: User = null

  create(title: string, description: string){
    this.apiService.post('/workplacements', {title, description}).subscribe((response: ApiResponse) => {
      this.messageService.pushMessage(response.message, 'successful', 5)
      this.router.navigate([`/workplacement/${response.id}`])
    })
  }

  getById(id: number){
    return this.apiService.get(`/workplacements/${id}`)
  }

  getMyWorkplacements(){
    return this.apiService.get('/workplacements/myworkplacements')
  }

  getMembers(workplacementId: number){
    return this.apiService.get(`/workplacements/members/${workplacementId}`)
  }

  addMember(workplacementId: number, username: string, canManageTasks: boolean){
    return this.apiService.post('/workplacements/addmember', {workplacementId, username, canManageTasks})
  }

  updateCurrentWorkplacementsMembers(workplacementId: number){
    this.getMembers(workplacementId).subscribe((response: User[]) => {
      this.currentWorkplacementsMembers = response
    })
  }

  canManageTasks(userId: number, workplacementId: number){
    return this.apiService.get(`/workplacements/canmanagetasks/${userId}/${workplacementId}`)
  }
}
