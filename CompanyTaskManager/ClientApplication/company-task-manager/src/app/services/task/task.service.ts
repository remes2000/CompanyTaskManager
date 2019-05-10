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
  public currentWorkplacementFreeTasks: Array<Task> = []
  public selectedTask: Task = null

  create(title: string, description: string, priority: string, employeeId: number, workplacementId: number){
    return this.apiService.post('/tasks', {title, description, priority, employeeId, workplacementId, addedById: this.authService.currentUserValue.userId})
  }

  updateTasks(workplacementId: number, userId: number){
    this.apiService.get(`/tasks/${workplacementId}/${userId}`).subscribe((res: Task[]) => {
      this.selectedUserTasks = res
    })
  }

  updateFreeTasks(workplacementId: number){
    this.apiService.get(`/tasks/${workplacementId}/freetasks`).subscribe((res: Task[]) => {
      this.currentWorkplacementFreeTasks = res
    })
  }

  selectTask(task: Task){
    this.selectedTask = task
  }

  changeStatus(taskId: number, status: string){
    return this.apiService.post(`/tasks/changestatus/${taskId}`, { status })
  }

  deleteTask(task: Task){
    return this.apiService.delete(`/tasks/${task.taskId}`)
  }

  assignFreeTask(taskId: number, employeeId: number){
    return this.apiService.put(`/tasks/assign/${taskId}/${employeeId}`)
  }

  markAsFree(taskId: number){
    return this.apiService.put(`/tasks/markasfree/${taskId}`)
  }
}
