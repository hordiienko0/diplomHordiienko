import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCompanyMemberComponent } from './add-company-member.component';

describe('AddCompanyMemberComponent', () => {
  let component: AddCompanyMemberComponent;
  let fixture: ComponentFixture<AddCompanyMemberComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddCompanyMemberComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddCompanyMemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
