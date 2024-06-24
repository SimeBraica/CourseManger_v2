import { TestBed } from '@angular/core/testing';

import { AcademicyearServiceService } from './academicyear-service.service';

describe('AcademicyearServiceService', () => {
  let service: AcademicyearServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AcademicyearServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
