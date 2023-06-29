import {Component, Inject, OnInit} from '@angular/core';
import {IMember} from "../resources/models/member.model";
import {addNewMember, loadRoles} from "../state/administration.actions";
import {select, Store} from "@ngrx/store";
import {MatIconRegistry} from "@angular/material/icon";
import {DomSanitizer} from "@angular/platform-browser";
import {MatDialogRef, MAT_DIALOG_DATA} from "@angular/material/dialog";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {IRole} from "../resources/models/role.model";
import {selectRoles} from "../state/administration.selectors";
import {Observable} from "rxjs";
import {Option} from "../../../shared/components/inputs/dropdown/dropdown.component";
import {map} from "rxjs/operators";


@Component({
  selector: 'app-add-company-member',
  templateUrl: './add-company-member.component.html',
  styleUrls: ['./add-company-member.component.scss']
})
export class AddCompanyMemberComponent {

  roles$: Observable<Option[]> = this.store.pipe(
    select(selectRoles),
    map(roles => roles == null
      ? []
      : roles.map<Option>(role => ({ value: role.name, viewValue: role.name, }))),
  );

  companyId: number

  firstName = new FormControl('', [Validators.required]);
  lastName = new FormControl('', [Validators.required]);
  userEmail = new FormControl('', [Validators.required, Validators.email]);
  roleName = new FormControl('', [Validators.required]);

  form = new FormGroup({
    firstName: this.firstName,
    lastName: this.lastName,
    userEmail: this.userEmail,
    roleName: this.roleName,
  });

  constructor(
    private store: Store,
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer,
    @Inject(MAT_DIALOG_DATA) public data: number) {

    this.matIconRegistry.addSvgIcon(
      'cross',
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/Cross.svg")
    );
    this.store.dispatch(loadRoles());
    this.companyId = this.data;
  }

  onSubmit() {
    this.form.markAllAsTouched();

    if (!this.form.valid) {
      return;
    }

    this.store.dispatch(addNewMember({
      data: {
        ...this.form.value,
        companyId: this.companyId
      } as IMember
    }));
  }

  firstNameError() {
    if (this.firstName.hasError('required')) {
      return 'Імя обовязкове';
    }
    return '';
  }

  lastNameError() {
    if (this.lastName.hasError('required')) {
      return 'Прізвище обовязкове';
    }
    return '';
  }

  userEmailError() {
    if (this.userEmail.hasError('required')) {
      return 'Email is required';
    }
    if (this.userEmail.hasError('email')) {
      return 'Будь ласка, введіть дійсну адресу електронної пошти';
    }
    return '';
  }

  roleNameError() {
    if (this.roleName.hasError('required')) {
      return 'Будь ласка, оберіть роль';
    }
    return '';
  }

}
