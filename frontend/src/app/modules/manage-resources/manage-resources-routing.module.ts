import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {ManageResourcesComponent} from "./manage-resources.component";
import {MaterialComponent} from "./material/material.component";

const routes: Routes = [
  {
    path: 'manage-resources',
    component: ManageResourcesComponent
  },
  {
    path: 'material',
    component: MaterialComponent
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManageResourcesRoutingModule{}
