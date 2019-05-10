import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteWorkplacementComponent } from './delete-workplacement.component';

describe('DeleteWorkplacementComponent', () => {
  let component: DeleteWorkplacementComponent;
  let fixture: ComponentFixture<DeleteWorkplacementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeleteWorkplacementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteWorkplacementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
