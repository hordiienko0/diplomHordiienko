import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationsRoutingModule } from './notifications-routing.module';
import { NotificationsComponent } from './notifications.component';
import { MatIconModule } from '@angular/material/icon';



@NgModule({
  declarations: [
    NotificationsComponent],
  imports: [
    CommonModule,
    NotificationsRoutingModule,
    MatIconModule
  ]
})
export class NotificationsModule { }
