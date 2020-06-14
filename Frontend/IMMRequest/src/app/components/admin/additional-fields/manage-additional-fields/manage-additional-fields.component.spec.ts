import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageAdditionalFieldsComponent } from './manage-additional-fields.component';

describe('ManageAdditionalFieldsComponent', () => {
  let component: ManageAdditionalFieldsComponent;
  let fixture: ComponentFixture<ManageAdditionalFieldsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageAdditionalFieldsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageAdditionalFieldsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
