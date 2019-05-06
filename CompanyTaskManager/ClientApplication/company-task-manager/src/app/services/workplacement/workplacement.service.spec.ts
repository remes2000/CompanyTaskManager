import { TestBed } from '@angular/core/testing';

import { WorkplacementService } from './workplacement.service';

describe('WorkplacementService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WorkplacementService = TestBed.get(WorkplacementService);
    expect(service).toBeTruthy();
  });
});
