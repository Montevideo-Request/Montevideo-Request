import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditAdditionalFieldComponent } from './edit-additional-field.component';

describe('EditAdditionalFieldComponent', () => {
  let component: EditAdditionalFieldComponent;
  let fixture: ComponentFixture<EditAdditionalFieldComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditAdditionalFieldComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditAdditionalFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
