import { Component, OnInit } from '@angular/core';
import { MatTabChangeEvent } from '@angular/material/tabs';
import { Route, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { select, Store } from '@ngrx/store';
import { map, Observable, tap } from 'rxjs';
import { CardInformation } from 'src/app/shared/components/card/card.component';
import { AppState } from 'src/app/store';
import { IProjectOverview } from './resources/models/project-overview';
import { ProjectStatus } from './resources/models/status';
import { changeParams, getDetailedProject, getProjectsWithParams } from './state/project.actions';
import { selectProjects } from './state/project.selectors';
import { openMenu, revealMenu } from "../../store/actions/menu.actions";
import { openModalDialog } from 'src/app/store/actions/modal-dialog.action';
import { AddProjectComponent } from './add-project/add-project.component';
import { Order } from './resources/models/order';
import { navigate } from 'src/app/store/actions/route.actions';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {
  projects$: Observable<IProjectOverview[]>
  status = ProjectStatus

  constructor(public dialog: MatDialog, private matIconRegistry: MatIconRegistry,
              private domSanitizer: DomSanitizer, private store: Store<AppState>) {
    this.matIconRegistry.addSvgIcon(
      'sort',
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/sort.svg")
    );
    this.projects$ = this.store.pipe(select(selectProjects));
    this.store.dispatch(openMenu());
    this.store.dispatch(revealMenu());
  }


  ngOnInit() {
    this.store.dispatch(getProjectsWithParams());
  }

  changeStatus(event: MatTabChangeEvent) {
    let index;
    if (event.index === 0) {
      index = 1;
    } else if (event.index === 1) {
      index = 0;
    } else if (event.index === 2) {
      index = 3;
    } else if (event.index === 3) {
      index = 2;
    } else {
      throw new Error("Unknown tab index");
    }

    this.store.dispatch(changeParams({ params: { status: index } }))
  }

  sortProjectsByName() {
    this.store.dispatch(changeParams({
      params: {
        sort: "ProjectName",
        order: Order.ASC
      }
    }))
  }

  sortProjectsByStartTime() {
    this.store.dispatch(changeParams({
      params: {
        sort: "StartTime",
        order: Order.ASC
      }
    }))
  }

  sortProjectsByStartTimeDesc() {
    this.store.dispatch(changeParams({
      params: {
        sort: "StartTime",
        order: Order.DESC
      }
    }))
  }

  onSearchQuery($event: string) {
    this.store.dispatch(changeParams({
      params: {
        query: $event
      }
    }))
  }

  getCardInformation(project: IProjectOverview): CardInformation {
    const completedPhases = project.phases.filter(p => p.isFinished)
    return {
      id: project.id,
      title: project.projectName,
      image: !project.imageUrl ? `/assets/images/placeholder.jpg` : (project.imageUrl.includes("http") ? project.imageUrl : `${environment.filesBaseUrl}/${project.imageUrl}`),
      date: `${project.startTime}-${project.endTime}`,
      subtitle: `${project.country}, ${project.city}, ${project.address}`,
      status: project.status,
      statusBarLabel: project.phases[completedPhases.length - 1]?.phaseName ?? ' ',
      statusBarProgress: completedPhases?.length,
      statusBarFull: project.phases.length
    }
  }

  add() {
    this.store.dispatch(openModalDialog({ component: AddProjectComponent }));
  }

  redirectToProjectPage(id: number) {
    this.store.dispatch(navigate({ commands: [`/projects/${id}`] }))
  }

}
