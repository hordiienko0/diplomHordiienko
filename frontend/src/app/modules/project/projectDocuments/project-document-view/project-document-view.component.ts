import { Component, OnInit, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store';
import { closeModalDialog } from 'src/app/store/actions/modal-dialog.action';
import { IProjectDocumentUpdate } from '../../resources/models/project-documents/project-document-update.model';
import * as fromProjectActions from '../../state/project.actions';
import { IProjectDocument } from "../../resources/models/project-documents/project-document.model";
import { environment } from "../../../../../environments/environment";
import { ProjectFileService } from "../../resources/services/project-file.services";

@Component({
  selector: 'app-project-document-view',
  templateUrl: './project-document-view.component.html',
  styleUrls: ['./project-document-view.component.scss']
})
export class ProjectDocumentViewComponent implements OnInit {

  constructor(
    @Inject(MAT_DIALOG_DATA) public projectDocument: IProjectDocument,
    private store: Store<AppState>,
    private projectFileService: ProjectFileService) {
  }

  ngOnInit(): void {
    const url = this.getFullPath();

    if (!this.isImage(url)) {
      this.downloadDocument(this.projectDocument);
      this.store.dispatch(closeModalDialog());
    }
  }

  getFullPath() {
    return environment.production
      ? this.projectDocument.link
      : `${environment.filesBaseUrl}/${this.projectDocument.link}`
  }

  isImage(url: string) {
    return url.endsWith('png') || url.endsWith('jpg') || url.endsWith('jpeg');
  }

  downloadDocument(document: { link: string, fileName: string }) {
    this.projectFileService.downloadFile(document.link, document.fileName);
  }
}
