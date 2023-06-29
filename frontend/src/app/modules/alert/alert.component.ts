import { Component, Inject, Input, OnInit } from '@angular/core';
import { MatSnackBarRef, MAT_SNACK_BAR_DATA} from '@angular/material/snack-bar';
import { AlertModel } from './resources/models/alert-model';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss']
})
export class AlertComponent {

  constructor(@Inject(MAT_SNACK_BAR_DATA) public data: AlertModel,
  public snackBarRef: MatSnackBarRef<AlertComponent>) { }
}
