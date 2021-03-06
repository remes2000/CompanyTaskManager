import { Component, OnInit } from '@angular/core';
import { WorkplacementService } from 'src/app/services/workplacement/workplacement.service';
import { Workplacement } from 'src/app/models/Workplacement';
import { ActivatedRoute } from '@angular/router'
import { User } from 'src/app/models/User';
import { AuthService } from 'src/app/services/auth/auth.service';
import { TaskService } from 'src/app/services/task/task.service';
import { Task } from 'src/app/models/Task';
import { PriorityPipe } from '../../pipes/priority.pipe';

@Component({
  selector: 'app-workplacement',
  templateUrl: './workplacement.component.html',
  styleUrls: ['./workplacement.component.scss']
})
export class WorkplacementComponent implements OnInit {

  constructor(
    public workplacementService: WorkplacementService,
    public authService: AuthService,
    public route: ActivatedRoute,
    public taskService: TaskService
  ) { }

  public idFromRoute: number = -1
  public workplacement: Workplacement = null
  public canManageTasks: boolean = false

  ngOnInit() {
    this.idFromRoute = this.route.snapshot.params['id']

    this.workplacementService.getById(this.idFromRoute).subscribe((workplacement: Workplacement) => {
      this.workplacement = workplacement
    })

    this.workplacementService.updateCurrentWorkplacementsMembers(this.idFromRoute)

    this.workplacementService.canManageTasks(this.authService.currentUserValue.userId, this.idFromRoute).subscribe((res: boolean) => {
      this.canManageTasks = res
    })

    this.workplacementService.selectedUser = this.authService.currentUserValue
    this.taskService.updateTasks(this.idFromRoute, this.workplacementService.selectedUser.userId)
  }

  selectUser(userId: number){
    this.workplacementService.showFreeTasks = false
    this.workplacementService.selectedUser = this.workplacementService.currentWorkplacementsMembers.filter(m => m.userId === userId)[0]

    this.taskService.updateTasks(this.idFromRoute, userId)
  }

  todoFilter(task: Task){
    return task.status === 'todo'
  }

  doingFilter(task: Task){
    return task.status === 'doing'
  }

  doneFilter(task: Task){
    return task.status === 'done'
  }

  sortByPriority(a: Task, b: Task){
    const pipe = new PriorityPipe()
    return  pipe.transform(b.priority, true) - pipe.transform(a.priority, true)
  }

  showFreeTasks(){
    this.workplacementService.showFreeTasks = true
    this.taskService.updateFreeTasks(this.idFromRoute)
  }
}
