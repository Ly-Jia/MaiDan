import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BillbookComponent } from './billbook.component';

describe('BillbookComponent', () => {
  let component: BillbookComponent;
  let fixture: ComponentFixture<BillbookComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BillbookComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BillbookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
