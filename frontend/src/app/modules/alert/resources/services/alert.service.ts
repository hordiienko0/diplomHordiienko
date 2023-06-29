import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AlertComponent } from '../../alert.component';
import { AlertType } from '../types/alert-type';

@Injectable({
  providedIn: AlertService
})
export class AlertService {

  constructor(private _snackBar: MatSnackBar) { }

  showAlert(message: string, buttonText: string, type: AlertType) {
    this._snackBar.openFromComponent(AlertComponent, {
      data: {
        message: message,
        buttonText: buttonText,
        type: type
      },
      horizontalPosition: 'right',
      verticalPosition: "top",
    })
  }
}
