import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AcademicyearDetailsComponentComponent } from './academicyear-details-component.component';

describe('AcademicyearDetailsComponentComponent', () => {
  let component: AcademicyearDetailsComponentComponent;
  let fixture: ComponentFixture<AcademicyearDetailsComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AcademicyearDetailsComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AcademicyearDetailsComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
