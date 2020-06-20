import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TypeBComponent } from './type-b.component';

describe('TypeBComponent', () => {
  let component: TypeBComponent;
  let fixture: ComponentFixture<TypeBComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TypeBComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TypeBComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
