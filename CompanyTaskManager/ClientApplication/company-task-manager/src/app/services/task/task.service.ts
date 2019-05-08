import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { AuthService } from '../auth/auth.service';
import { MessagesService } from '../messages/messages.service';
import { Task } from 'src/app/models/Task';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(
    private apiService: ApiService,
    private authService: AuthService,
    private messageService: MessagesService
  ) { }

  public selectedUserTasks: Array<Task> = []
  public selectedTask: Task = null

  create(title: string, description: string, priority: string, employeeId: number, workplacementId: number){
    return this.apiService.post('/tasks', {title, description, priority, employeeId, workplacementId, addedById: this.authService.currentUserValue.userId})
  }

  updateTasks(workplacementId: number, userId: number){
    this.apiService.get(`/tasks/${workplacementId}/${userId}`).subscribe((res: Task[]) => {
      this.selectedUserTasks = res
    })
  }

  selectTask(task: Task){
    this.selectedTask = task
  }

  changeStatus(taskId: number, status: string){
    return this.apiService.post(`/tasks/changestatus/${taskId}`, { status })
  }
}
