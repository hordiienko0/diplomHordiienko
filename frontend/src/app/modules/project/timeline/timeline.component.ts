import {Component, Input, OnInit} from '@angular/core';
import {TaskFieldsModel} from "@syncfusion/ej2-angular-gantt";
import {MatDialog} from "@angular/material/dialog";
import {AddPhaseDialogComponent} from "../add-phase-dialog/add-phase-dialog.component";
import {Observable} from "rxjs";
import {IPhase} from "../resources/models/phase.model";
import {select, Store} from "@ngrx/store";
import {AppState} from "../../../store";
import {
  selectCurrentProject,
  selectCurrentProjectId,
  selectProjectInformation,
  selectProjectPhases
} from "../state/project.selectors";
import {IProjectDetailed} from "../resources/models/project-details";

@Component({
  selector: 'app-timeline',
  templateUrl: './timeline.component.html',
  styleUrls: ['./timeline.component.scss']
})
export class TimelineComponent implements OnInit {

  phases$ : Observable<IPhase[]>
  currentProject$ : Observable<IProjectDetailed>

  constructor(private dialog : MatDialog,
              private store : Store<AppState>) {
    this.phases$ = store.pipe(
      select(selectProjectPhases)
    );
    this.currentProject$ = store.pipe(select(selectProjectInformation));
  }

  ngOnInit() {
  }

  onAddClick() {
    this.dialog.open(AddPhaseDialogComponent, {
      data: null
    });
  }

  menuSelected = "Month"

  onMenuChanged(changes: string) {
    this.menuSelected = changes;
  }

  onEditRequested(phase: IPhase) {
    this.dialog.open(AddPhaseDialogComponent, {
      data: phase
    });
  }
}
