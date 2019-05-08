import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayTaskComponent } from './display-task.component';

describe('DisplayTaskComponent', () => {
  let component: DisplayTaskComponent;
  let fixture: ComponentFixture<DisplayTaskComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DisplayTaskComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DisplayTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
