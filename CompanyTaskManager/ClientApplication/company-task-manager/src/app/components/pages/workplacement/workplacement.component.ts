import { Component, OnInit } from '@angular/core';
import { WorkplacementService } from 'src/app/services/workplacement/workplacement.service';
import { Workplacement } from 'src/app/models/Workplacement';
import { ActivatedRoute } from '@angular/router'
import { User } from 'src/app/models/User';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-workplacement',
  templateUrl: './workplacement.component.html',
  styleUrls: ['./workplacement.component.scss']
})
export class WorkplacementComponent implements OnInit {

  constructor(
    private workplacementService: WorkplacementService,
    private authService: AuthService,
    private route: ActivatedRoute
  ) { }

  private idFromRoute: number = -1
  private workplacement: Workplacement = null
  private canManageTasks: boolean = false

  ngOnInit() {
    this.idFromRoute = this.route.snapshot.params['id']

    this.workplacementService.getById(this.idFromRoute).subscribe((workplacement: Workplacement) => {
      this.workplacement = workplacement
    })

    this.workplacementService.updateCurrentWorkplacementsMembers(this.idFromRoute)

    this.workplacementService.canManageTasks(this.authService.currentUserValue.id, this.idFromRoute).subscribe((res: boolean) => {
      this.canManageTasks = res
    })

    this.workplacementService.selectedUser = this.authService.currentUserValue
  }

  selectUser(userId: number){
    this.workplacementService.selectedUser = this.workplacementService.currentWorkplacementsMembers.filter(m => m.id === userId)[0]
  }

}
