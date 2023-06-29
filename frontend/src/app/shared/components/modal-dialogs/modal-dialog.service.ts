import {Injectable} from "@angular/core";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {ComponentType} from "@angular/cdk/overlay";

@Injectable({
  providedIn: "root"
})

export class ModalDialogService {
  constructor(private _dialog: MatDialog) {
  }


  showDialog(component: ComponentType<any>, config?:MatDialogConfig) {
    this._dialog.open(component, config)
  }


  hideDialog() {
    this._dialog.closeAll();
  }
}

