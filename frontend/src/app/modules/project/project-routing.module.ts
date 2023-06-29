import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import { ProjectDashboardComponent } from './project-dashboard/project-dashboard.component';
import { ProjectDescriptionComponent } from './project-description/project-description.component';
import { ProjectComponent } from './project.component';

const routes : Routes = [
    {
        path: 'projects',
        component: ProjectComponent
    },
    {
      path: 'project-dashboard', component: ProjectDashboardComponent
    },
    {
      path: 'projects/:id',
      component: ProjectDashboardComponent
    }
]


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectRoutingModule { }
