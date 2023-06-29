import { Component, Input, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store';
import { openModalDialog } from 'src/app/store/actions/modal-dialog.action';
import { IProjectDocumentUpdate } from '../../resources/models/project-documents/project-document-update.model';
import { IProjectDocument } from '../../resources/models/project-documents/project-document.model';
import * as fromProjectActions from '../../state/project.actions'
import { ProjectDocumentRenameComponent } from '../project-document-rename/project-document-rename.component';
import { ProjectDocumentViewComponent } from "../project-document-view/project-document-view.component";
@Component({
  selector: 'app-project-document-overview',
  templateUrl: './project-document-overview.component.html',
  styleUrls: ['./project-document-overview.component.scss'],
})
export class ProjectDocumentOverviewComponent implements OnInit {
  @Input() projectDocument?: IProjectDocument
  menuClicked = false;
  constructor(
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer,
    private store: Store<AppState>
  ) {}

  ngOnInit(): void {
  }
  menuClick(){
    this.menuClicked = !this.menuClicked;
  }
  onDelete(id: number){
    this.store.dispatch(fromProjectActions.deleteProjectDocument({projectDocumentId: id}))
  }
  onView(){
    this.store.dispatch(openModalDialog({component: ProjectDocumentViewComponent, config: {
        data: this.projectDocument,
        panelClass: 'viewport-100'
      }}));
  }
  onRename(fileName: string, id: number){
    this.store.dispatch(openModalDialog({component: ProjectDocumentRenameComponent, config:{
      data: {name: fileName, id: id} as IProjectDocumentUpdate
    }}))
  }
  getFileExtension(link: string){
    return link.split('.').pop();
  }
}
