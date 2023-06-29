import {NgModule} from "@angular/core";
import {ManageResourcesComponent} from "./manage-resources.component";
import {MaterialComponent} from './material/material.component';
import {ManageResourcesRoutingModule} from "./manage-resources-routing.module";
import {MatTabsModule} from "@angular/material/tabs";
import {ManageResourcesApiService} from "../manage-resources/resources/services/manage-resources-api.service";
import {StoreModule} from "@ngrx/store";
import {EffectsModule} from "@ngrx/effects";
import {ManageResourcesEffects} from "./state/manage-resources.effects";
import {MatIconModule} from "@angular/material/icon";
import {SharedModule} from "../../shared/shared.module";
import {AsyncPipe, CommonModule, CurrencyPipe} from "@angular/common";
import {ReactiveFormsModule} from "@angular/forms";
import {MatTableModule} from "@angular/material/table";
import {MatButtonModule} from "@angular/material/button";
import * as formManage from "./state/manage-resources.reducer";
import {NgrxFormsModule} from "ngrx-forms";
import {ClipboardModule} from "@angular/cdk/clipboard";
import {NgxSpinnerModule} from "ngx-spinner";
import {ServiceListComponent} from "./service-list/service-list.component";


@NgModule({
    imports: [
        CommonModule,

        ManageResourcesRoutingModule,
        CommonModule,
        MatTabsModule,
        StoreModule.forFeature(
            formManage.manageResourceFeatureKey,
            formManage.reducer
        ),
        EffectsModule.forFeature([ManageResourcesEffects]),
        MatIconModule,
        SharedModule,
        AsyncPipe,
        ReactiveFormsModule,
        MatTableModule,
        CurrencyPipe,
        MatButtonModule,

        EffectsModule.forFeature([ManageResourcesEffects]),
        NgxSpinnerModule,
        ClipboardModule
    ],
  declarations: [
    ManageResourcesComponent,
    MaterialComponent,
    ServiceListComponent
  ],
  exports: [
    ServiceListComponent,
    ManageResourcesComponent
  ],
  providers: [ManageResourcesApiService],

})

export class ManageResourcesModule {
}
