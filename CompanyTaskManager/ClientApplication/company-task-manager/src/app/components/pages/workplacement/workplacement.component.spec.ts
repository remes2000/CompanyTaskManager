import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkplacementComponent } from './workplacement.component';

describe('WorkplacementComponent', () => {
  let component: WorkplacementComponent;
  let fixture: ComponentFixture<WorkplacementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WorkplacementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkplacementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
