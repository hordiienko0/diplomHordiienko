import {NgModule} from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {IsLoggedInGuard} from "../../core/resources/guards/is-logged-in.guard";
import { NotificationsComponent } from './notifications.component';

const routes: Routes = [
  {
    path: 'notifications',
    component: NotificationsComponent,
    canActivate: [IsLoggedInGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NotificationsRoutingModule {
}
