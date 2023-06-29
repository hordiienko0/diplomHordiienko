import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatSidenavModule} from "@angular/material/sidenav";
import {MatListModule} from "@angular/material/list";
import {MatDialogModule} from '@angular/material/dialog';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {StoreModule} from '@ngrx/store';
import * as fromAdministration from './state/administration.reducer';
import {EffectsModule} from '@ngrx/effects';
import {AdministrationEffects} from './state/administration.effects';
import {MatIconModule} from "@angular/material/icon";
import {CompanyListComponent} from './company-list/company-list.component';
import {AdministrationApiService} from "./resources/services/administration-api.service";
import {MatMenuModule} from "@angular/material/menu";
import {ReactiveFormsModule} from "@angular/forms";
import {AdministrationRoutingModule} from './administration-routing.module';
import {FormsModule} from "@angular/forms";
import {CompanyInformationComponent} from './company-information/company-information.component';
import {ScrollingModule} from '@angular/cdk/scrolling';
import {MatCardModule} from '@angular/material/card';
import {SharedModule} from "../../shared/shared.module";
import {NgrxFormsModule} from 'ngrx-forms';
import { AddMemberFailureComponent } from './add-member-failure/add-member-failure.component';
import { AddCompanyMemberComponent } from './add-company-member/add-company-member.component';
import {AdministrationFileService} from "./resources/services/administration-file.service";

@NgModule({
  declarations: [
    CompanyListComponent,
    AddCompanyMemberComponent,
    CompanyInformationComponent,
    AddMemberFailureComponent
  ],
  exports: [
    CompanyInformationComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    MatSidenavModule,
    MatListModule,
    MatDialogModule,
    MatFormFieldModule,
    MatButtonModule,
    MatSelectModule,
    MatInputModule,
    StoreModule.forFeature(fromAdministration.administrationFeatureKey, fromAdministration.reducer),
    EffectsModule.forFeature([AdministrationEffects]),
    MatIconModule,
    MatMenuModule,
    ReactiveFormsModule,
    AdministrationRoutingModule,
    FormsModule,
    MatIconModule,
    ScrollingModule,
    MatCardModule,
    NgrxFormsModule,
  ],
  providers: [
    AdministrationApiService,
    AdministrationFileService
  ]
})
export class AdministrationModule {
}
