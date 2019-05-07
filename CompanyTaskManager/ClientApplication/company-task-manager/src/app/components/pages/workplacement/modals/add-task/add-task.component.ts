import { Component, OnInit } from '@angular/core';
import { MessagesService } from 'src/app/services/messages/messages.service';
import { WorkplacementService } from 'src/app/services/workplacement/workplacement.service';

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.scss']
})
export class AddTaskComponent implements OnInit {

  constructor(
    private messageService: MessagesService,
    private workplacementService: WorkplacementService
  ) { }

  private title: string = ''
  private description: string = ''
  private priority: string = 'normal'

  ngOnInit() {
  }

  addTask(){
    if(this.title === '' || this.description === ''){
      this.messageService.pushMessage('Uzupełnij wszystkie pola formularza!', 'error', 5)
    }
  }
}
