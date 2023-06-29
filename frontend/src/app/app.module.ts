import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {StoreModule} from '@ngrx/store';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {StoreDevtoolsModule} from '@ngrx/store-devtools';
import {environment} from '../environments/environment';
import {StoreRouterConnectingModule} from '@ngrx/router-store';
import {EffectsModule} from '@ngrx/effects';
import {RouterModule} from '@angular/router';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {metaReducers, reducers} from './store';
import {SpinnerEffects} from './store/effects/spinner.effects';
import {AlertEffects} from './store/effects/alert.effects';
import {RouteEffects} from './store/effects/route.effects';
import {ModalEffects} from './store/effects/modal.effects';
import {AuthModule} from './modules/auth/auth.module';
import {MyEntityModule} from './modules/my-entity/my-entity.module';
import {AdministrationModule} from "./modules/administration/administration.module";
import {CreateCompany} from './modules/administration/create-company/create-company.component';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatRippleModule} from '@angular/material/core';
import {AlertModule} from './modules/alert/alert.module';
import {NgxSpinnerModule} from 'ngx-spinner';
import {MenuComponent} from './core/shell/menu/menu.component';
import {MatIconModule} from "@angular/material/icon";
import {MatSidenavModule} from "@angular/material/sidenav";
import {LandingPageComponent} from './core/shell/landing-page/landing-page.component';
import {SharedModule} from './shared/shared.module';
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatMomentDateModule} from "@angular/material-moment-adapter";
import {MatTabsModule} from "@angular/material/tabs";
import {ProjectModule} from "./modules/project/project.module";
import {CompanyModule} from "./modules/company/company.module";
import {MatDialogModule} from "@angular/material/dialog";
import {MenuEffects} from './store/effects/menu.effects';
import {AdministrationEffects} from './modules/administration/state/administration.effects';
import {NotificationService} from './shared/services/notification.service';
import {GanttModule} from "@syncfusion/ej2-angular-gantt";
import {NotificationsEffects} from './store/effects/notifi.effects';
import {NotificationsModule} from './modules/notifications/notifications.module';
import {ManageResourcesModule} from "./modules/manage-resources/manage-resources.module";

@NgModule({
  declarations: [
    AppComponent,
    CreateCompany,
    MenuComponent,
    LandingPageComponent
  ],
  imports: [
    SharedModule,
    BrowserModule,
    AppRoutingModule,
    StoreModule.forRoot(reducers, {
      metaReducers,
      runtimeChecks: {
        strictStateImmutability: true,
        strictActionImmutability: false,
        strictStateSerializability: true,
        strictActionSerializability: true,
        strictActionWithinNgZone: true,
        strictActionTypeUniqueness: true,
      }
    }),
    BrowserAnimationsModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    AuthModule,
    MyEntityModule,
    AlertModule,
    StoreDevtoolsModule.instrument({maxAge: 25, logOnly: environment.production}),
    StoreRouterConnectingModule.forRoot(),
    EffectsModule.forRoot([
      SpinnerEffects,
      AlertEffects,
      RouteEffects,
      ModalEffects,
      AdministrationEffects,
      NotificationsEffects
    ]),
    AdministrationModule,
    ProjectModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatRippleModule,
    NgxSpinnerModule.forRoot({type: 'ball-scale-multiple'}),
    MatIconModule,
    MatSidenavModule,
    MatDatepickerModule,
    MatMomentDateModule,
    MatTabsModule,
    CompanyModule,
    EffectsModule.forFeature([MenuEffects]),
    GanttModule,
    NotificationsModule,
    ManageResourcesModule
  ],
  providers: [NotificationService],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {
}
