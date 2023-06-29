import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { AlertComponent } from './alert.component';
import { AlertService } from './resources/services/alert.service';
import { MatButtonModule } from '@angular/material/button';



@NgModule({
  declarations: [
    AlertComponent
  ],
  imports: [
    CommonModule,
    MatSnackBarModule,
    MatButtonModule,
  ],
  exports: [
    AlertComponent
  ],
  providers: [
    AlertService
  ]
})
export class AlertModule { }
