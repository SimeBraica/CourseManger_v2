import { TestBed } from '@angular/core/testing';

import { StudyprogramServiceService } from './studyprogram-service.service';

describe('StudyprogramServiceService', () => {
  let service: StudyprogramServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudyprogramServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
