import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-add-workplacement',
  templateUrl: './add-workplacement.component.html',
  styleUrls: ['./add-workplacement.component.scss']
})
export class AddWorkplacementComponent implements OnInit {

  constructor() { }

  private title: string = ''
  private description: string = ''

  ngOnInit() {
  }

  createWorkplacement(){
    alert('test')
  }
}
