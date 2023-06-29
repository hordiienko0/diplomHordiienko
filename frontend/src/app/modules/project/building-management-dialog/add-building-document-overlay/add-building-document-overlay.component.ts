import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ProjectFileService } from "../../resources/services/project-file.services";
import { Store } from "@ngrx/store";
import { AppState } from "../../../../store";
import { NgxDropzoneChangeEvent } from "ngx-dropzone";
import * as fromProjectActions from "../../state/project.actions";
import { FormControl } from "@angular/forms";

@Component({
  selector: 'app-add-building-document-overlay',
  templateUrl: './add-building-document-overlay.component.html',
  styleUrls: ['./add-building-document-overlay.component.scss']
})
export class AddBuildingDocumentOverlay {

  @Input() buildingId: number = 0;
  @Input() projectId: number = 0;
  @Output() cancel = new EventEmitter<void>();
  @Output() submit = new EventEmitter<void>();

  files: File[] = [];

  linkFormControl = new FormControl<string>('');

  constructor(
    private filesService: ProjectFileService,
    private store: Store<AppState>
  ) {
  }

  get link() {
    return this.linkFormControl.value ?? '';
  }

  onSelect(event: NgxDropzoneChangeEvent) {
    this.files.push(...event.addedFiles);
  }

  onSubmit() {
    if (this.files.length === 0 && this.link === '') {
      return;
    }

    this.store.dispatch(fromProjectActions.uploadProjectDocuments({
      buildingId: this.buildingId,
      projectId: this.projectId,
      files: this.files,
      urls: [this.link],
    }));

    this.submit.emit();
  }

  onCancel() {
    this.cancel.emit();
    this.files = [];
  }

  onRemove(event: File) {
    this.files.splice(this.files.indexOf(event), 1);
  }

  get dropzoneStyle() {
    return this.files.length > 0
      ? {}
      : {
        visibility: 'hidden',
        height: 0,
        border: 0,
      };
  }
}
