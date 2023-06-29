import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Store } from '@ngrx/store';
import {
  ImageCroppedEvent,
  LoadedImage,
  base64ToFile,
} from 'ngx-image-cropper';
import { AppState } from 'src/app/store';
import { closeModalDialog } from 'src/app/store/actions/modal-dialog.action';
import { CompanyFileService } from '../recources/services/company-file.service';
import * as fromCompanyActions from '../state/company.actions';

@Component({
  selector: 'app-company-image-crop',
  templateUrl: './company-image-crop.component.html',
  styleUrls: ['./company-image-crop.component.scss'],
})
export class CompanyImageCropComponent implements OnInit {
  link: string = '';
  companyId?: number;
  croppedImage: any = '';
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private store: Store<AppState>,
    private fileService: CompanyFileService
  ) {
    this.link = data.link;
    this.companyId = data.companyId;
  }

  ngOnInit(): void {}

  cancel() {
    this.store.dispatch(closeModalDialog());
  }
  submit() {
    if (this.companyId) {
      let file = this.blobToFile(base64ToFile(this.croppedImage));
      this.fileService.putLogo(this.companyId, file).toPromise().then(() => {
        this.store.dispatch(fromCompanyActions.uploadCompanyLogoSuccess())
      }).catch(()=>
        this.store.dispatch(fromCompanyActions.uploadCompanyLogoFailure())
      );
    }
    this.store.dispatch(closeModalDialog());
  }

  blobToFile(blob: Blob): File {
    let fileName = this.link.split('/').pop();
    if (!fileName) fileName = 'image.png';
    return new File([blob], fileName);
  }

  imageLoaded(image: LoadedImage) {
    this.store.dispatch(fromCompanyActions.loadCropImageFinish());
    this.croppedImage = image;
  }

  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = event.base64;
  }
}
