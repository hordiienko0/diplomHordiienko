import {Component, Inject, Input, OnInit} from '@angular/core';
import {FormControl, Validators} from "@angular/forms";
import {IPhase} from "../resources/models/phase.model";
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {select, Store} from "@ngrx/store";
import {AppState} from "../../../store";
import {addPhaseToProject, deletePhase, editPhase} from "../state/project.actions";
import * as moment from 'moment';
import {Observable, retry} from "rxjs";
import {
  selectCurrentProject,
  selectProjectBuildings,
  selectProjectInformation,
  selectProjectPhases
} from "../state/project.selectors";
import {map} from "rxjs/operators";
import {IPhaseStep} from "../resources/models/phase-step.model";
import {closeModalDialog} from "../../../store/actions/modal-dialog.action";
import {IBuilding} from "../resources/models/building.model";
import {showCustomAlert} from "../../../store/actions/alert.actions";
import {IProjectDetailed} from "../resources/models/project-details";

class DialogData {
}

@Component({
  selector: 'app-add-phase-dialog',
  templateUrl: './add-phase-dialog.component.html',
  styleUrls: ['./add-phase-dialog.component.scss']
})
export class AddPhaseDialogComponent implements OnInit {

  phaseToEdit: IPhase;
  phaseSteps: IPhaseStep[] = [];
  projectBuildings$: Observable<IBuilding[]>;
  currentProject$: Observable<IProjectDetailed>;

  // phase controls
  nameControl: FormControl | null = null;
  startDateControl: FormControl | null = null;
  endDateControl: FormControl | null = null;

  // phase step control
  nameStepControl = new FormControl('', [Validators.required]);
  startDateStepControl = new FormControl('', [Validators.required]);
  endDateStepControl = new FormControl('', [Validators.required]);

  isSectionOpened = false;
  isBuildingOpened = false;

  constructor(@Inject(MAT_DIALOG_DATA) public data: DialogData,
              private store: Store<AppState>) {
    this.phaseToEdit = data as IPhase;
    if (this.phaseToEdit != null) {
      this.phaseSteps = [...this.phaseToEdit.phaseSteps];
    }
    this.nameControl = new FormControl(this.phaseToEdit?.phaseName, [Validators.required]);
    if (this.phaseToEdit != null) {
      this.startDateControl = new FormControl(new Date(this.phaseToEdit?.startDate), [Validators.required]);
      this.endDateControl = new FormControl(new Date(this.phaseToEdit?.endDate), [Validators.required]);
    } else {
      this.startDateControl = new FormControl('', [Validators.required]);
      this.endDateControl = new FormControl('', [Validators.required]);
    }

    this.projectBuildings$ = this.store.pipe(select(selectProjectBuildings));
    this.currentProject$ = this.store.pipe(select(selectProjectInformation));
  }

  ngOnInit(): void {
  }

  onSubmit(project: IProjectDetailed) {
    if (this.startDateControl?.value >= this.endDateControl?.value) {
      this.store.dispatch(showCustomAlert({
        alert: {
          message: "Start date should be lesser than end date",
          type: "error",
          buttonText: "Ok"
        }
      }));
      return;
    }

    if (project.startTime > this.startDateControl?.value || project.endTime < this.endDateControl?.value) {
      this.store.dispatch(showCustomAlert({
        alert: {
          message: "Phase should be in terms of project",
          type: "error",
          buttonText: "Ok"
        }
      }));
      return;
    }

    if (this.nameControl?.invalid) {
      return;
    }

    if (this.phaseSteps.length == 0) {
      this.store.dispatch(showCustomAlert({
        alert: {
          message: "Phase steps should be at least one",
          type: "error",
          buttonText: "Ok"
        }
      }));
      return;
    }

    let phase = {
      phaseName: this.nameControl?.value,
      startDate: moment(this.startDateControl?.value).toISOString(),
      endDate: moment(this.endDateControl?.value).toISOString(),
      phaseSteps: this.phaseSteps
    } as IPhase;

    if (this.phaseToEdit == null) {
      this.store.dispatch(addPhaseToProject({phase: phase}));
    } else {
      phase.id = this.phaseToEdit.id;
      this.store.dispatch(editPhase({phase: phase}));
    }

    this.store.dispatch(closeModalDialog());
  }

  onDeleteClick() {
    this.store.dispatch(deletePhase({id: this.phaseToEdit.id}));
    this.store.dispatch(closeModalDialog());
  }

  onStepSubmit() {
    if (this.startDateStepControl.value! >= this.endDateStepControl.value!) {
      this.store.dispatch(showCustomAlert({
        alert: {
          message: "Start date should be lesser than end date",
          type: "error",
          buttonText: "Ok"
        }
      }));
      return;
    }

    if (this.startDateControl?.value > this.startDateStepControl.value! || this.endDateControl?.value < this.endDateStepControl.value!) {
      this.store.dispatch(showCustomAlert({
        alert: {
          message: "Phase step should be in terms of phase",
          type: "error",
          buttonText: "Ok"
        }
      }));
      return;
    }

    if (this.nameStepControl.invalid) {
      return;
    }

    let phaseStep = {
      phaseStepName: this.nameStepControl.value,
      startDate: this.startDateStepControl.value,
      endDate: this.endDateStepControl.value,
      isDone: false
    } as IPhaseStep;

    this.phaseSteps.push(phaseStep);

    this.nameStepControl.reset();
    this.startDateStepControl.reset();
    this.endDateStepControl.reset();

    this.isSectionOpened = false;
  }

  isBuildingInPhaseSteps(buildingId: number): boolean {
    return !!this.phaseSteps.find(step => step.buildingId == buildingId);
  }

  onBuildingCheckboxChanged(checked: boolean, building: IBuilding) {
    if (checked) {
      this.phaseSteps.push({
        phaseStepName: building.buildingName,
        buildingId: building.id
      } as IPhaseStep)
    } else {
      let phaseStep = this.phaseSteps.find(step => step.buildingId == building.id);
      let index = this.phaseSteps.indexOf(phaseStep!)
      this.phaseSteps.splice(index, 1);
    }
  }

  onSubsectionChecked(checked: boolean, subsection: IPhaseStep) {
    console.log(subsection);
    let index = this.phaseSteps.indexOf(subsection);
    this.phaseSteps.splice(index, 1);
    this.phaseSteps.push({...subsection, isDone: checked});
  }

  onSubsectionDelete(step: IPhaseStep) {
    let index = this.phaseSteps.indexOf(step);
    this.phaseSteps.splice(index, 1);
  }
}
