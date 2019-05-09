import { Component, OnInit, ViewChild } from '@angular/core';
import { TaskService } from 'src/app/services/task/task.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { ApiResponse } from 'src/app/models/ApiResponse';
import { MessagesService } from 'src/app/services/messages/messages.service';
import { ModalComponent } from 'src/app/components/modal/modal.component';
import { WorkplacementService } from 'src/app/services/workplacement/workplacement.service';

@Component({
  selector: 'app-display-task',
  templateUrl: './display-task.component.html',
  styleUrls: ['./display-task.component.scss']
})
export class DisplayTaskComponent implements OnInit {

  constructor(
    private taskService: TaskService,
    private authService: AuthService,
    private messageService: MessagesService,
    private workplacementService: WorkplacementService
  ) { }

  @ViewChild("displayTaskModal") displayTaskModal: ModalComponent;
  private canManageTasks: boolean = false

  ngOnInit() {
    this.workplacementService.canManageTasks(this.authService.currentUserValue.userId, this.taskService.selectedTask.workplacementId).subscribe((res: boolean) => {
      this.canManageTasks = res
    })
  }

  changeStatus(status: string){
    this.taskService.changeStatus(this.taskService.selectedTask.taskId, status).subscribe((res: ApiResponse) => {
      if(res.message){
        this.messageService.pushMessage(res.message, 'successful', 5)
        this.displayTaskModal.closeModal('displayTaskModal')
        this.taskService.updateTasks(this.taskService.selectedTask.workplacementId, this.taskService.selectedTask.employeeId)
      }
    })
  }

  deleteTask(){
    this.taskService.deleteTask(this.taskService.selectedTask).subscribe((res: ApiResponse) => {
      if(!res.message)
        return
      
      this.messageService.pushMessage(res.message, 'successful', 5)
      this.taskService.updateTasks(this.taskService.selectedTask.workplacementId, this.taskService.selectedTask.employeeId)
      this.displayTaskModal.closeModal('displayTaskModal')
    })
  }
}
