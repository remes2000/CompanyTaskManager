import { Component, OnInit, ViewChild } from '@angular/core';
import { WorkplacementService } from 'src/app/services/workplacement/workplacement.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { User } from 'src/app/models/User';
import { MessagesService } from 'src/app/services/messages/messages.service';
import { ApiResponse } from 'src/app/models/ApiResponse';
import { ModalComponent } from 'src/app/components/modal/modal.component';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-delete-employee',
  templateUrl: './delete-employee.component.html',
  styleUrls: ['./delete-employee.component.scss']
})
export class DeleteEmployeeComponent implements OnInit {

  constructor(
    private workplacementService: WorkplacementService,
    private authService: AuthService,
    private messageService: MessagesService,
    private route: ActivatedRoute
  ) { }

  @ViewChild("deleteEmployeeModal") deleteEmployeeModal: ModalComponent;
  private selectedUser: User = null
  private workplacementId: number = -1

  ngOnInit() {
    this.workplacementId = this.route.snapshot.params['workplacementId']
  }

  select(member: User){
    this.selectedUser = member
  }

  deleteMembers(){
    if(this.selectedUser === null)
      return this.messageService.pushMessage('Zaznacz pracownika, którego chcesz usunąć', 'error', 5);

    const shouldDelete = confirm("Czy napewno chcesz usunąć tego pracownika?")
    if(!shouldDelete)
      return

    this.workplacementService.removeMember(this.workplacementId, this.selectedUser.userId).subscribe((res: ApiResponse) => {
      if(!res.message)
        return
      
      this.messageService.pushMessage(res.message, 'successful', 5)
      this.deleteEmployeeModal.closeModal({submitted: true})
      this.workplacementService.updateCurrentWorkplacementsMembers(this.workplacementId)
    })
  }

}
