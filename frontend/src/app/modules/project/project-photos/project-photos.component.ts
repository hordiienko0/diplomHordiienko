import { Component, Input, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { IProjectPhoto as IProjectPhoto } from '../resources/models/project-photo.model';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { select, Store } from '@ngrx/store';
import { AppState } from 'src/app/store';
import * as fromProjectSelectors from '../state/project.selectors';
import * as fromProjectActions from '../state/project.actions';
@Component({
  selector: 'app-project-photos',
  templateUrl: './project-photos.component.html',
  styleUrls: ['./project-photos.component.scss'],
})
export class ProjectPhotosComponent implements OnInit {
  constructor(
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer,
    private store: Store<AppState>
  ) {}
  @Input() width = 500;
  @Input() height = 300;
  @Input() projecId?:number;
  projectPhotos$?: Observable<IProjectPhoto[]>;
  fileBaseUrl = environment.filesBaseUrl;
  
  ngOnInit(): void {
    this.addSvgIcons();
    if(this.projecId)
    this.store.dispatch(fromProjectActions.loadProjectPhotos({ projectId: this.projecId }));
    
    this.projectPhotos$ = this.store.pipe(
      select(fromProjectSelectors.selectProjectPhotos)
    );
  }

  deleteProjectPhoto(id: number) {
    this.store.dispatch(
      fromProjectActions.deleteProjectPhoto({ photoId: id, projectId: this.projecId as number })
    );
  }

  addSvgIcons() {
    this.matIconRegistry.addSvgIcon(
      'caret_left',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        'assets/icons/caret-left.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      'trash',
      this.domSanitizer.bypassSecurityTrustResourceUrl('assets/icons/trash.svg')
    );
    this.matIconRegistry.addSvgIcon(
      'caret_right',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        'assets/icons/caret-right.svg'
      )
    );
  }
}
