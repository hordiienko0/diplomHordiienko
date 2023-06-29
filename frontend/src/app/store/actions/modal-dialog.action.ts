import { createAction, props } from "@ngrx/store";
import {ComponentType} from "@angular/cdk/overlay";
import {MatDialogConfig} from "@angular/material/dialog";

export const openModalDialog = createAction(
  '[ModalDialogOpened] Modal Dialog opened',
  props<{component: ComponentType<any>, config?:MatDialogConfig}>()
)
export const closeModalDialog = createAction(
  '[ModalDialogClosed] Modal Dialog closed'
);
