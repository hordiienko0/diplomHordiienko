import { Component, OnInit, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store';
import { closeModalDialog } from 'src/app/store/actions/modal-dialog.action';
import { IProjectDocumentUpdate } from '../../resources/models/project-documents/project-document-update.model';
import * as fromProjectActions from '../../state/project.actions';

@Component({
  selector: 'app-project-document-rename',
  templateUrl: './project-document-rename.component.html',
  styleUrls: ['./project-document-rename.component.scss']
})
export class ProjectDocumentRenameComponent implements OnInit {

  name = new FormControl('', [Validators.required]);
  form = new FormGroup({fileName: this.name});
  constructor( @Inject(MAT_DIALOG_DATA) public data: IProjectDocumentUpdate, private store: Store<AppState>) { }

  ngOnInit(): void {
    this.form.get('fileName')?.setValue(this.data.name);
    console.log(this.form)
  }
  submit(){
    const updateModel ={id: this.data.id, name: this.name.value} as IProjectDocumentUpdate
    this.store.dispatch(fromProjectActions.updateProjectDocument({model: updateModel}))
  }

  cancel(){
    this.store.dispatch(closeModalDialog())
  }
}
