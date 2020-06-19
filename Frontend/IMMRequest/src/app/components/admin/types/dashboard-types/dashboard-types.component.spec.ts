import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardTypesComponent } from './dashboard-types.component';

describe('DashboardTypesComponent', () => {
  let component: DashboardTypesComponent;
  let fixture: ComponentFixture<DashboardTypesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DashboardTypesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
