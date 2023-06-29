import { Component, Input, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { AppState } from 'src/app/store';
import { IProjectDocument } from '../../resources/models/project-documents/project-document.model';
import * as fromProjectActions from '../../state/project.actions'
import * as fromProjectSelectors from '../../state/project.selectors'
@Component({
  selector: 'app-project-documents',
  templateUrl: './project-documents.component.html',
  styleUrls: ['./project-documents.component.scss']
})
export class ProjectDocumentsComponent implements OnInit {

  projectDocuments$ = this.store.select(fromProjectSelectors.selectProjectDocuments);
  constructor( private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer, private store: Store<AppState>) { }
    
  ngOnInit(): void {
    this.addSvgIcons();
  }
  
  addSvgIcons() {
    this.matIconRegistry.addSvgIcon(
      'file_csv',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        'assets/icons/fileCsv.svg'
      )
    );
  }
}
