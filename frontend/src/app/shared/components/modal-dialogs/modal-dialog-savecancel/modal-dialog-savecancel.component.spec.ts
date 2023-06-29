import { ComponentFixture, TestBed } from '@angular/core/testing';
import {ModalDialogSaveCancelComponent} from "./modal-dialog-savecancel.component";


describe('ModalDialogSaveCancelComponent', () => {
  let component: ModalDialogSaveCancelComponent;
  let fixture: ComponentFixture<ModalDialogSaveCancelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalDialogSaveCancelComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalDialogSaveCancelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
