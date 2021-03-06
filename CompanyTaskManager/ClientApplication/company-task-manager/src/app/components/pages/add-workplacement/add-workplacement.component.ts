import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api/api.service';
import { WorkplacementService } from 'src/app/services/workplacement/workplacement.service';

@Component({
  selector: 'app-add-workplacement',
  templateUrl: './add-workplacement.component.html',
  styleUrls: ['./add-workplacement.component.scss']
})
export class AddWorkplacementComponent implements OnInit {

  constructor(
    public workplacementService: WorkplacementService
  ) { }

  public title: string = ''
  public description: string = ''

  ngOnInit() {
  }

  createWorkplacement(){
    this.workplacementService.create(this.title, this.description)
  }
}
