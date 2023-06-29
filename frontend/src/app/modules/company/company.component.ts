import {Component, OnInit} from '@angular/core';
import {MatIconRegistry} from "@angular/material/icon";
import {DomSanitizer} from "@angular/platform-browser";
import {select, Store} from "@ngrx/store";
import {AppState} from "../../store";
import * as fromCompanyActions from "./state/company.actions";
import {selectCompany, selectCompanyProfileForm, selectProjects} from "./state/company.selectors";
import {Observable} from "rxjs";
import {ICompanyProfile} from "./recources/models/company-profile";
import {openMenu, revealMenu} from "../../store/actions/menu.actions";
import {IProjectOverview} from "./recources/models/project-overview";
import {FormGroupState} from "ngrx-forms";
import {CompanyProfileFormValue} from "./recources/forms/company-profile-form";
import { openModalDialog } from 'src/app/store/actions/modal-dialog.action';
import { CompanyImageCropComponent } from './company-image-crop/company-image-crop.component';
import { CompanyFileService } from './recources/services/company-file.service';
import { getImageLink } from '../administration/resources/utils';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.scss'],
})
export class CompanyComponent implements OnInit {
  company$?: Observable<ICompanyProfile>;
  projects$: Observable<IProjectOverview[]>;
  companyProfileForm$: Observable<FormGroupState<CompanyProfileFormValue>>

  constructor(
    iconRegistry: MatIconRegistry,
    sanitizer: DomSanitizer,
    private store: Store<AppState>,
    private companyFileService: CompanyFileService
  ) {
    iconRegistry.addSvgIcon(
      'pencil',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/pencil.svg')
    );
    this.store.dispatch(openMenu());
    this.store.dispatch(revealMenu());
    this.company$ = this.store.pipe(select(selectCompany));
    this.projects$ = this.store.pipe(select(selectProjects));
    this.companyProfileForm$ = this.store.pipe(select(selectCompanyProfileForm));
  }

  ngOnInit(): void {
    this.store.dispatch(fromCompanyActions.loadCompany());
  }

  enableEditing() {
    this.store.dispatch(fromCompanyActions.enableEditingCompanyProfileForm());
  }

  getFullImageUrl(imageUrl: string) {
    return !imageUrl ?  `assets/images/placeholder.jpg` : imageUrl.includes("http") ? imageUrl : `${environment.filesBaseUrl}/${imageUrl}`
  }

  disableEditing() {
    this.store.dispatch(fromCompanyActions.cancelEditingCompanyProfileForm());
  }

  onSubmit(address: string, email: string, website: string) {
    this.store.dispatch(fromCompanyActions.submitEditingCompanyProfileForm({
      email: email,
      address: address,
      website: website
    }));
  }
  uploadLogo(event: Event, companyId: number) {
    const target = event.target as HTMLInputElement;
    if (target.files !== null) {
      const file = target.files[0];
      this.companyFileService.putLogo(companyId, file).toPromise().then(() => {
        this.store.dispatch(fromCompanyActions.uploadCompanyLogoSuccess())
      }).catch(()=>
        this.store.dispatch(fromCompanyActions.uploadCompanyLogoFailure())
      );
    }
  }

  editLogo(link: string, companyId: number) {
    this.store.dispatch(openModalDialog({
      component: CompanyImageCropComponent, config: {
        data: { link: link, companyId: companyId }
      }
    }))
    this.store.dispatch(fromCompanyActions.loadCropImageStart());
  }

  deleteLogo(){
    this.store.dispatch(fromCompanyActions.deleteCompanyLogo());
  }

  isLogoEmpty(logo: string){
    return logo === '';
  }

  getImage(image: string){
    return getImageLink(image);
  }
}
