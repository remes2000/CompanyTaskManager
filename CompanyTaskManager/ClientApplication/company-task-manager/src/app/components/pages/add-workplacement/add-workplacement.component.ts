import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api/api.service';

@Component({
  selector: 'app-add-workplacement',
  templateUrl: './add-workplacement.component.html',
  styleUrls: ['./add-workplacement.component.scss']
})
export class AddWorkplacementComponent implements OnInit {

  constructor(
    private apiService: ApiService
  ) { }

  private title: string = ''
  private description: string = ''

  ngOnInit() {
  }

  createWorkplacement(){
    alert('test')
  }
}
