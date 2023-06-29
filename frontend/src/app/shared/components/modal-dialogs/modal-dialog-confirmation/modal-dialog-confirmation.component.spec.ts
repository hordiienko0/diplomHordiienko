import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalDialogConfirmationComponent } from './modal-dialog-confirmation.component';

describe('ModalDialogConfirmationComponent', () => {
  let component: ModalDialogConfirmationComponent;
  let fixture: ComponentFixture<ModalDialogConfirmationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalDialogConfirmationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalDialogConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
