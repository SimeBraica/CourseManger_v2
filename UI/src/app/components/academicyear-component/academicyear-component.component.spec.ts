import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AcademicyearComponentComponent } from './academicyear-component.component';

describe('AcademicyearComponentComponent', () => {
  let component: AcademicyearComponentComponent;
  let fixture: ComponentFixture<AcademicyearComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AcademicyearComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AcademicyearComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
