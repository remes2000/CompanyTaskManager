import { Component, OnInit } from '@angular/core';
import { WorkplacementService } from 'src/app/services/workplacement/workplacement.service';
import { Workplacement } from 'src/app/models/Workplacement';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  constructor(
    private workplacementService: WorkplacementService
  ) { }

  private workplacements: Workplacement[] = []

  ngOnInit() {
    this.workplacementService.getMyWorkplacements().subscribe((response: Workplacement[]) => {
      this.workplacements = response
    })
  }

}
