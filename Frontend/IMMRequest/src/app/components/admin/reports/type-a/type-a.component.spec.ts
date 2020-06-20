import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TypeAComponent } from './type-a.component';

describe('TypeAComponent', () => {
  let component: TypeAComponent;
  let fixture: ComponentFixture<TypeAComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TypeAComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TypeAComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
