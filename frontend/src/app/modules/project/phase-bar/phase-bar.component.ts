import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { AppState } from 'src/app/store';
import { openModalDialog } from 'src/app/store/actions/modal-dialog.action';
import { AddProjectPhotosComponent } from '../add-project-photos/add-project-photos.component';
import { ActivatedRoute } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { FormControlState, FormGroupState } from 'ngrx-forms';
import { Observable } from 'rxjs';
import { IProjectDetailed } from '../resources/models/project-details';
import {
  cancelEditProjectInformationForm,
  editProjectInformationForm,
  getDetailedProject,
  submitProjectInformationForm
} from '../state/project.actions';
import { selectProjectInformation, selectProjectInformationForm } from '../state/project.selectors';
import * as fromProjectInformationForm from '../resources/forms/project-information-form';
import { IProjectUpdate } from '../resources/models/project-update';

@Component({
  selector: 'app-phase-bar',
  templateUrl: './phase-bar.component.html',
  styleUrls: ['./phase-bar.component.scss'],
})
export class PhaseBarComponent {

  @Input() phases: { name: string, isFinished: boolean }[] = [];

  get lastFinished(): number {
    const find = this.phases.findIndex(p => !p.isFinished);
    return find == -1 ? this.phases.length - 1 : find - 1;
  }

  getStyles(index: number) {
    return {
      'z-index': (this.phases.length - index),
      'width': (100 / 5) - 2 + '%',
      '--color': index == this.lastFinished + 1
        ? '#FFEBD3'
        : index <= this.lastFinished
          ? '#FFBD93'
          : '#FFF6EA',
    };
  }
}
