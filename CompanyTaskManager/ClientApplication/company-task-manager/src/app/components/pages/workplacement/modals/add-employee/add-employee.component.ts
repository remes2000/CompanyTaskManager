import { Component, OnInit } from '@angular/core';
import { MessagesService } from 'src/app/services/messages/messages.service';
import { WorkplacementService } from 'src/app/services/workplacement/workplacement.service';
import { ApiResponse } from 'src/app/models/ApiResponse';
import { Route, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.scss']
})
export class AddEmployeeComponent implements OnInit {

  constructor(
    private messagesService: MessagesService,
    private workplacementService: WorkplacementService,
    private route: ActivatedRoute
  ) { }

  private username : string = ''
  private canManageTasks: boolean = false
  private workplacementId: number = -1

  ngOnInit() {
    this.workplacementId = this.route.snapshot.params['workplacementId']
  }

  clearForm(){
    this.username = ''
    this.canManageTasks = false
  }

  addUser(){
    if(this.username === ''){
      this.messagesService.pushMessage('Wpisz nazwę użytkownika!', 'error', 3)
    }

    this.workplacementService.addMember(this.workplacementId, this.username, this.canManageTasks).subscribe((res: ApiResponse) => {
      if(res.error){
        this.messagesService.pushMessage(res.error, 'error', 5)
        return
      }

      this.messagesService.pushMessage(res.message, 'successful', 5)
      this.workplacementService.updateCurrentWorkplacementsMembers(this.workplacementId)
      this.clearForm()
    })
  }

}
