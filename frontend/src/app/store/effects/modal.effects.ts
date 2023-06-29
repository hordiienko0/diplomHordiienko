import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import * as modalDialogAction from '../actions/modal-dialog.action';
import * as fromAuthActions from '../actions/auth.actions';
import * as fromAdministrationActions from '../../modules/administration/state/administration.actions';
import {tap} from 'rxjs/operators';
import {MatDialog} from "@angular/material/dialog";
import {ModalDialogService} from "../../shared/components/modal-dialogs/modal-dialog.service";



@Injectable()
export class ModalEffects {
  hideModal$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(
          fromAuthActions.loginSuccess,
          fromAdministrationActions.CreateCompanySuccess,
          fromAdministrationActions.addNewMemberSuccess),
        tap(() => {
          this.dialogService.closeAll();
        })
      ),
    {dispatch: false}
  );

  openModalDialog$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(modalDialogAction.openModalDialog),
        tap((action) => {
            this.modalDialogService.showDialog(action.component, action.config)
          }
        )
      ),
    {dispatch: false}
  )

  hideModalDialog$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(modalDialogAction.closeModalDialog),
        tap(() => {
            this.modalDialogService.hideDialog()
          }
        )
      ),
    {dispatch: false}
  )

  constructor(private actions$: Actions, private dialogService: MatDialog, private modalDialogService: ModalDialogService) {
  }
}
