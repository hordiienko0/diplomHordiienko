import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DatepickerHeaderComponent } from './datepicker-header.component';

describe('DatepickerHeaderComponent', () => {
  let component: DatepickerHeaderComponent<any>;
  let fixture: ComponentFixture<DatepickerHeaderComponent<any>>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DatepickerHeaderComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DatepickerHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
