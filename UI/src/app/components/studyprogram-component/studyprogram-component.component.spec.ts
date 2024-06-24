import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudyprogramComponentComponent } from './studyprogram-component.component';

describe('StudyprogramComponentComponent', () => {
  let component: StudyprogramComponentComponent;
  let fixture: ComponentFixture<StudyprogramComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StudyprogramComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(StudyprogramComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
