import { Component, Input, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { AppState } from 'src/app/store';
import { openModalDialog } from 'src/app/store/actions/modal-dialog.action';
import { AddProjectPhotosComponent } from '../add-project-photos/add-project-photos.component';
import { select, Store } from '@ngrx/store';
import { FormGroupState } from 'ngrx-forms';
import { Observable } from 'rxjs';
import { IProjectDetailed } from '../resources/models/project-details';
import {
  cancelEditProjectInformationForm,
  editProjectInformationForm,
  submitProjectInformationForm
} from '../state/project.actions';
import { selectProjectInformation, selectProjectInformationForm } from '../state/project.selectors';
import * as fromProjectInformationForm from '../resources/forms/project-information-form';
import { IProjectUpdate } from '../resources/models/project-update';
import { AddProjectTeamComponent } from "../add-project-team/add-project-team.component";
import * as fromProjectActions from '../state/project.actions';
import * as fromProjectSelectors from '../state/project.selectors';
import { map } from "rxjs/operators";
import { IPhaseOverviewDTO } from "../resources/models/phase-overview";

@Component({
  selector: 'app-project-description[projectId]',
  templateUrl: './project-description.component.html',
  styleUrls: ['./project-description.component.scss'],
})
export class ProjectDescriptionComponent implements OnInit {

  @Input() projectId: number = 0;

  project$?: Observable<IProjectDetailed>
  projectInformationForm$?: Observable<FormGroupState<fromProjectInformationForm.ProjectInformationFormValue>>;

  isEditEnabled = false

  teamMembers$ = this.store.select(fromProjectSelectors.selectCurrentProjectTeam);

  hasTeamMembers$ = this.teamMembers$.pipe(
    map(members => members && members.length > 0)
  );

  constructor(
    private store: Store<AppState>,
    iconRegistry: MatIconRegistry,
    sanitizer: DomSanitizer,
  ) {
    iconRegistry.addSvgIcon(
      'pencil',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/pencil.svg')
    );
    iconRegistry.addSvgIcon(
      'plus',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/plus.svg')
    );
    iconRegistry.addSvgIcon(
      'copy',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/copy.svg')
    );
  }

  addPhotosModal(id: number) {
    this.store.dispatch(
      openModalDialog({
        component: AddProjectPhotosComponent, config: {
          data: id
        }
      }))
    this.projectInformationForm$ = this.store.pipe(
      select(selectProjectInformationForm)
    );
  }

  changeEdit(event: Event) {
    event.preventDefault()
    this.isEditEnabled = !this.isEditEnabled
  }


  onFormEdit() {
    this.isEditEnabled = true;
    this.store.dispatch((editProjectInformationForm()));
  }

  onCancel() {
    this.isEditEnabled = false;
    this.store.dispatch(cancelEditProjectInformationForm());
  }

  ngOnInit(): void {
    this.project$ = this.store.pipe(select(selectProjectInformation))
    this.projectInformationForm$ = this.store.pipe(select(selectProjectInformationForm))

    this.store.dispatch(fromProjectActions.getProjectTeam({ projectId: this.projectId }));
  }

  onSave(id: number, form: fromProjectInformationForm.ProjectInformationFormValue) {
    this.isEditEnabled = false;

    this.store.dispatch(
      submitProjectInformationForm({
        id: id,
        address: form.address,
        startTime: form.startTime,
        endTime: form.endTime
      } as IProjectUpdate)
    );
  }

  addTeamModal() {
    this.store.dispatch(
      openModalDialog({
        component: AddProjectTeamComponent, config: {
          data: {
            projectId: this.projectId,
          },
          autoFocus: false,
        },
      })
    );
  }

  mapPhases(phases: IPhaseOverviewDTO[]): { name: string, isFinished: boolean }[] {
    return phases
      .map(phase => {
        return {
          name: phase.phaseName,
          isFinished: phase.isFinished,
        }
      });
  }

}
