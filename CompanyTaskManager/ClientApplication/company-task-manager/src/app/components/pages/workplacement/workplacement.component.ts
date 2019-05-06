import { Component, OnInit } from '@angular/core';
import { WorkplacementService } from 'src/app/services/workplacement/workplacement.service';
import { Workplacement } from 'src/app/models/Workplacement';
import { ActivatedRoute } from '@angular/router'

@Component({
  selector: 'app-workplacement',
  templateUrl: './workplacement.component.html',
  styleUrls: ['./workplacement.component.scss']
})
export class WorkplacementComponent implements OnInit {

  constructor(
    private workplacementService: WorkplacementService,
    private route: ActivatedRoute
  ) { }

  private idFromRoute: number = -1
  private workplacement: Workplacement = null

  ngOnInit() {
    this.idFromRoute = this.route.snapshot.params['id']

    this.workplacementService.getById(this.idFromRoute).subscribe((workplacement: Workplacement) => {
      this.workplacement = workplacement
    })

  }

}
