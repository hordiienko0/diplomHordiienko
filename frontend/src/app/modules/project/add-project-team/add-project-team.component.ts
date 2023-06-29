import { Component, Inject } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store';
import * as fromProjectActions from "../state/project.actions";
import * as fromProjectSelectors from "../state/project.selectors";
import { MatIconRegistry } from "@angular/material/icon";
import { DomSanitizer } from "@angular/platform-browser";
import { ProjectService } from "../resources/services/project.services";
import { GetCompanyUsersUserDto } from "../resources/models/get-company-users-dto";
import { MatCheckbox, MatCheckboxChange } from "@angular/material/checkbox";
import { tap } from "rxjs/operators";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";

@Component({
  selector: 'app-add-project-team',
  templateUrl: './add-project-team.component.html',
  styleUrls: ['./add-project-team.component.scss'],
})
export class AddProjectTeamComponent {

  isOverlayOpen = false;

  teamMembers$ = this.store.select(fromProjectSelectors.selectCurrentProjectTeam).pipe(
    tap(users => {
        this.teamMembersSnapshot = users ?? [];
        this.updateTeamMembersToSubmit();
      }
    )
  );

  searchCompanyUsers$ = this.projectService.getCompanyUsers(null, null);

  teamMembersSnapshot: { id: number, name: string, role: string, email: string, phoneNumber: string }[] = [];
  teamMembersToSubmit: { id: number, name: string, role: string, email: string, phoneNumber: string }[] = [];
  selectedUsers: GetCompanyUsersUserDto[] = [];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { projectId: number },
    private store: Store<AppState>,
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer,
    private projectService: ProjectService) {

    iconRegistry.addSvgIcon(
      'cross',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/Cross.svg')
    );

    iconRegistry.addSvgIcon(
      'delete',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/delete.svg')
    );
  }

  updateTeamMembersToSubmit() {
    if (!this.teamMembersSnapshot) {
      return;
    }
    const array = [...this.teamMembersSnapshot];

    for (const user of this.selectedUsers) {
      array.push({
        id: user.id,
        name: `${user.firstName} ${user.lastName}`,
        role: user.role,
        email: user.email,
        phoneNumber: user.phoneNumber,
      });
    }

    this.teamMembersToSubmit = array;
  }

  isSelected(userId: number): boolean {
    return !!this.selectedUsers.find(u => u.id == userId);
  }

  search(event: any) {
    const filter = event.data
    this.searchCompanyUsers$ = this.projectService.getCompanyUsers(filter, null);
  }

  submit() {
    this.store.dispatch(fromProjectActions.setProjectTeam({
      projectId: this.data.projectId,
      userIds: this.teamMembersToSubmit.map(m => m.id),
    }))
  }

  submitSelected() {
    this.isOverlayOpen = false;
    this.updateTeamMembersToSubmit();
  }

  cancelSelected() {
    this.isOverlayOpen = false;
    this.updateTeamMembersToSubmit();
  }

  selectionChange(event: MatCheckboxChange, user: GetCompanyUsersUserDto) {
    if (event.checked) {
      this.selectUser(event.source, user);
      return;
    }
    this.unselectUser(event.source, user);
  }

  selectUser(checkbox: MatCheckbox, user: GetCompanyUsersUserDto) {
    this.selectedUsers.push(user);
  }

  unselectUser(checkbox: MatCheckbox, user: GetCompanyUsersUserDto) {
    this.selectedUsers = this.selectedUsers.filter(u => u.id != user.id);
  }

  removeUser(userId: number) {
    this.selectedUsers = this.selectedUsers.filter(u => u.id != userId);
    this.teamMembersToSubmit = this.teamMembersToSubmit.filter(u => u.id != userId);
  }

}
