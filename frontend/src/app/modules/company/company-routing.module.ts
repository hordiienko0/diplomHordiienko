import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {CompanyComponent} from "./company.component";
import {IsLoggedInGuard} from "../../core/resources/guards/is-logged-in.guard";
import {IsInRoleGuard} from "../../core/resources/guards/is-in-role.guard";
import {UserRole} from "../auth/resources/models/userRole";

const routes: Routes = [
  {
    path: 'company-profile',
    component: CompanyComponent,
    canActivate: [IsLoggedInGuard, IsInRoleGuard],
    data: {roles: [UserRole.OperationalManager]}
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompanyRoutingModule {
}
