import { TestBed } from '@angular/core/testing';

import { AcademicyearDetailsServiceService } from './academicyear-details-service.service';

describe('AcademicyearDetailsServiceService', () => {
  let service: AcademicyearDetailsServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AcademicyearDetailsServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
