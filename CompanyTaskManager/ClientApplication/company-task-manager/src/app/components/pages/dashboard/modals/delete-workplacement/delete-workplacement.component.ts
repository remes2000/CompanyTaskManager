import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';
import { MessagesService } from 'src/app/services/messages/messages.service';
import { WorkplacementService } from 'src/app/services/workplacement/workplacement.service';
import { Workplacement } from 'src/app/models/Workplacement';
import { ApiResponse } from 'src/app/models/ApiResponse';
import { ModalComponent } from 'src/app/components/modal/modal.component';

@Component({
  selector: 'app-delete-workplacement',
  templateUrl: './delete-workplacement.component.html',
  styleUrls: ['./delete-workplacement.component.scss']
})
export class DeleteWorkplacementComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private messagesService: MessagesService,
    private workplacementService: WorkplacementService
  ) { }

  private workplacements: Workplacement[] = []
  private selectedWorkplacement: Workplacement = null
  @ViewChild("deleteWorkplacementModal") deleteWorkplacementModal: ModalComponent;

  ngOnInit() {
    this.workplacementService.getWorkplacementsOwnedBy(this.authService.currentUserValue.userId).subscribe((res: Workplacement[]) => {
      this.workplacements = res
    })
  }

  deleteWorkplacement(){
    if(this.selectedWorkplacement === null)
      return this.messagesService.pushMessage('Zaznacz miejsce pracy które chcesz usunąć!', 'error', 5)

    const shouldDelete = confirm("Czy napewno chcesz usunąć to miejsce pracy?")
    if(!shouldDelete)
      return

    this.workplacementService.deleteWorkplacement(this.selectedWorkplacement.workplacementId).subscribe((res: ApiResponse) => {
      if(!res.message)
        return

      this.messagesService.pushMessage("Miejsce pracy zostało pomyślnie usunięte!", "successful", 5)
      this.deleteWorkplacementModal.closeModal({submitted: true})
      this.workplacementService.updateMyWorkplacements()
    })
  }

}
