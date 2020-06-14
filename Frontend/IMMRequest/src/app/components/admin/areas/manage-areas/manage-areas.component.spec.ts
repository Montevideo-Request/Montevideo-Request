import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageAreasComponent } from './manage-areas.component';

describe('ManageAreasComponent', () => {
  let component: ManageAreasComponent;
  let fixture: ComponentFixture<ManageAreasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageAreasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageAreasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
