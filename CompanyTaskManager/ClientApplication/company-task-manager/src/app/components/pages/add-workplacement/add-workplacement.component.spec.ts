import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddWorkplacementComponent } from './add-workplacement.component';

describe('AddWorkplacementComponent', () => {
  let component: AddWorkplacementComponent;
  let fixture: ComponentFixture<AddWorkplacementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddWorkplacementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddWorkplacementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
