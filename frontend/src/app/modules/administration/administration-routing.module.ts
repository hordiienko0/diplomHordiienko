import {NgModule} from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {CompanyListComponent} from "./company-list/company-list.component";
import {CreateCompany} from "./create-company/create-company.component";
import {CompanyInformationComponent} from "./company-information/company-information.component";
import {IsLoggedInGuard} from "../../core/resources/guards/is-logged-in.guard";
import {IsInRoleGuard} from "../../core/resources/guards/is-in-role.guard";
import {UserRole} from "../auth/resources/models/userRole";

const routes: Routes = [
  {path: 'administration', redirectTo: 'company-list'},
  {
    path: 'company-list',
    component: CompanyListComponent,
    canActivate: [IsLoggedInGuard],
    data: {roles: [UserRole.Admin] }
  },
  {
    path: 'company-information/:id',
    component: CompanyInformationComponent,
    canActivate: [IsLoggedInGuard, IsInRoleGuard],
    data: {roles: [UserRole.Admin] }
  },
  {
    path: 'new-company',
    component: CreateCompany,
    canActivate: [IsLoggedInGuard, IsInRoleGuard],
    data: {roles: [UserRole.Admin] }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministrationRoutingModule {
}
