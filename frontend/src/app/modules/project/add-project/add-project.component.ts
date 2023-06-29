import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { NumberGenarateSevice } from 'src/app/shared/services/numberGenarate.services';
import { AppState } from 'src/app/store';
import { CreateProjectDTO } from '../resources/models/createProjectDTO';
import { RangeDateValidators } from '../resources/validators/rangeDateValidators.directive';
import * as fromProjectActions  from "../state/project.actions";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { TokenService } from "../../auth/resources/services/token.service";


@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.scss']
})
export class AddProjectComponent implements OnInit {
  isSubmited = false

  projectId: number
  form = new FormGroup({
    name: new FormControl<string>('', [Validators.required]),
    address: new FormControl<string>('', [Validators.required]),
    startDate: new FormControl<Date>(new Date(), [Validators.required]),
    endDate: new FormControl<Date>(new Date(), []),
  }, { validators: RangeDateValidators });

  constructor(private store: Store<AppState>,
              private numberGenarate: NumberGenarateSevice) {
    this.projectId = numberGenarate.generateId()
  }
  ngOnInit(): void {
  }

  get name() {
    return this.form.controls.name as FormControl;
  }
  get address() {
    return this.form.controls.address as FormControl;
  }
  get startDate() {
    return this.form.controls.startDate as FormControl<Date>;
  }
  get endDate() {
    return this.form.controls.endDate as FormControl<Date>;
  }

  submit() {
    console.log(this.form)
    if (this.form.invalid) {
      this.isSubmited = true;
      return
    }
    this.isSubmited = false;

    const project: CreateProjectDTO = {
      projectId: this.projectId,
      name: this.name.value,
      address: this.address.value,
      startDate: this.startDate.value.toJSON(),
      endDate: this.endDate.value.toJSON(),
    }

    this.store.dispatch(fromProjectActions.createProject({ project }))

  }
}
