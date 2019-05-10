import { Component, OnInit, ViewChild } from '@angular/core';
import { MessagesService } from 'src/app/services/messages/messages.service';
import { WorkplacementService } from 'src/app/services/workplacement/workplacement.service';
import { TaskService } from 'src/app/services/task/task.service';
import { ActivatedRoute } from '@angular/router';
import { ApiResponse } from 'src/app/models/ApiResponse';
import { ModalComponent } from 'src/app/components/modal/modal.component';

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.scss']
})
export class AddTaskComponent implements OnInit {

  constructor(
    private messageService: MessagesService,
    private workplacementService: WorkplacementService,
    private taskService: TaskService,
    private route: ActivatedRoute
  ) { }

  @ViewChild("addTaskModal") addTaskModal: ModalComponent;
  private title: string = ''
  private description: string = ''
  private priority: string = 'normal'

  private workplacementId: number = -1
  private userId: number = -1

  ngOnInit() {
    this.workplacementId = this.route.snapshot.params['workplacementId']
    this.userId = this.route.snapshot.params['userId']
  }

  addTask(){
    if(this.title === '' || this.description === ''){
      this.messageService.pushMessage('Uzupełnij wszystkie pola formularza!', 'error', 5)
    }

    this.taskService.create(this.title, this.description, this.priority, this.userId, this.workplacementId).subscribe((res: ApiResponse) => {
      this.messageService.pushMessage(res.message, 'successful', 5)
      if(this.userId != -1)
        this.taskService.updateTasks(this.workplacementId, this.userId)
      else
        this.taskService.updateFreeTasks(this.workplacementId)
      this.addTaskModal.closeModal({submitted: true})
    })
  }
}
