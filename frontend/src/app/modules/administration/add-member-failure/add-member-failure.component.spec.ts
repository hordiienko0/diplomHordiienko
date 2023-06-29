import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMemberFailureComponent } from './add-member-failure.component';

describe('AddMemberFailureComponent', () => {
  let component: AddMemberFailureComponent;
  let fixture: ComponentFixture<AddMemberFailureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddMemberFailureComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddMemberFailureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
