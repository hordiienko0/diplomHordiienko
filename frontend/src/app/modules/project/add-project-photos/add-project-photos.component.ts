import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Store } from '@ngrx/store';
import { catchError } from 'rxjs';
import { AppState } from 'src/app/store';
import { closeModalDialog } from 'src/app/store/actions/modal-dialog.action';
import { ProjectFileService } from '../resources/services/project-file.services';
import * as fromProjectActions from '../state/project.actions';
import { NgxDropzoneChangeEvent } from 'ngx-dropzone';
@Component({
  selector: 'app-add-project-photos',
  templateUrl: './add-project-photos.component.html',
  styleUrls: ['./add-project-photos.component.scss'],
})
export class AddProjectPhotosComponent implements OnInit {
  files: File[] = [];
  constructor(
    private filesService: ProjectFileService,
    @Inject(MAT_DIALOG_DATA) public data: number,
    private store: Store<AppState>
  ) {}

  ngOnInit(): void {}

  onSelect(event: NgxDropzoneChangeEvent) {
    console.log(event);
    this.files.push(...event.addedFiles);
  }

  submit() {
    if (this.files.length !== 0) {
      this.filesService.putPhotos(this.data, this.files).toPromise().then((result) => {
        this.store.dispatch(
          fromProjectActions.uploadProjecPhotoSuccess({ id: this.data })
        );
        this.store.dispatch(closeModalDialog());
      }).catch((error: any)=>{
        this.store.dispatch(fromProjectActions.uploadProjecPhotoFailure()),
        this.store.dispatch(closeModalDialog());
      });
    }
  }

  onRemove(event: File) {
    console.log(event);
    this.files.splice(this.files.indexOf(event), 1);
  }
}
