import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import {
  MAT_MOMENT_DATE_ADAPTER_OPTIONS,
  MatMomentDateModule,
  MomentDateAdapter
} from "@angular/material-moment-adapter";
import { MatAutocompleteModule } from "@angular/material/autocomplete";
import { MatButtonModule } from "@angular/material/button";
import { MatChipsModule } from "@angular/material/chips";
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from "@angular/material/core";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatSelectModule } from "@angular/material/select";
import { MatTabsModule } from "@angular/material/tabs";
import { RouterModule } from "@angular/router";
import { BlockWithIdComponent } from "./components/blocks/blockWithId/block-with-id/block-with-id.component";
import { BackButtonComponent } from "./components/buttons/back-button/back-button.component";
import { ButtonComponent } from "./components/buttons/button/button.component";
import { CloseButtonComponent } from "./components/buttons/close-button/close-button.component";
import { FilterComponent } from "./components/buttons/filter/filter.component";
import { LargeButtonComponent } from "./components/buttons/large-button/large-button.component";
import { MiniButtonComponent } from "./components/buttons/mini-button/mini-button.component";
import { UnderlinedButtonComponent } from "./components/buttons/underlined-button/underlined-button.component";
import { CardComponent } from "./components/card/card.component";
import { HasRoleComponent } from "./components/guards/has-role/has-role.component";
import { LoggedInComponent } from "./components/guards/logged-in/logged-in.component";
import { NotLoggedInComponent } from "./components/guards/not-logged-in/not-logged-in.component";
import { AutocompleteComponent } from "./components/inputs/autocomplete/autocomplete.component";
import { DatepickerHeaderComponent } from "./components/inputs/datepicker/datepicker-header/datepicker-header.component";
import { DatepickerComponent, MY_FORMATS } from "./components/inputs/datepicker/datepicker.component";
import { DropdownComponent } from "./components/inputs/dropdown/dropdown.component";
import { InputComponent } from "./components/inputs/input/input.component";
import { PasswordInputComponent } from "./components/inputs/password-input/password-input.component";
import { TabsComponent } from "./components/inputs/tabs/tabs.component";
import { ModalDialogConfirmationComponent } from "./components/modal-dialogs/modal-dialog-confirmation/modal-dialog-confirmation.component";
import {
  ModalDialogSaveCancelComponent
} from "./components/modal-dialogs/modal-dialog-savecancel/modal-dialog-savecancel.component";
import {NumberGenarateSevice} from "./services/numberGenarate.services";
import { AlertComponent } from "./components/misc/alert/alert.component";
import {NgrxFormsModule} from "ngrx-forms";
import { FilterInputComponent } from "./components/inputs/filter-input/filter-input.component";
import { DefaultAutocompleteComponent } from './components/inputs/default-autocomplete/default-autocomplete.component';


@NgModule({
  declarations: [
    CloseButtonComponent,
    BackButtonComponent,
    InputComponent,
    PasswordInputComponent,
    ButtonComponent,
    LargeButtonComponent,
    MiniButtonComponent,
    FilterComponent,
    DatepickerComponent,
    UnderlinedButtonComponent,
    DatepickerHeaderComponent,
    AutocompleteComponent,
    DropdownComponent,
    TabsComponent,
    CardComponent,
    FilterInputComponent,
    LoggedInComponent,
    NotLoggedInComponent,
    HasRoleComponent,
    ModalDialogSaveCancelComponent,
    ModalDialogConfirmationComponent,
    BlockWithIdComponent,
    AlertComponent,
    DefaultAutocompleteComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatIconModule,
    HttpClientModule,
    MatDatepickerModule,
    MatMomentDateModule,
    MatAutocompleteModule,
    MatChipsModule,
    MatSelectModule,
    MatTabsModule,
    NgrxFormsModule
  ],
  exports: [
    BlockWithIdComponent,
    CloseButtonComponent,
    BackButtonComponent,
    InputComponent,
    PasswordInputComponent,
    ButtonComponent,
    LargeButtonComponent,
    MiniButtonComponent,
    UnderlinedButtonComponent,
    FilterComponent,
    DatepickerComponent,
    MatMomentDateModule,
    AutocompleteComponent,
    DropdownComponent,
    TabsComponent,
    CardComponent,
    FilterInputComponent,
    LoggedInComponent,
    NotLoggedInComponent,
    HasRoleComponent,
    ModalDialogSaveCancelComponent,
    ModalDialogConfirmationComponent,
    AlertComponent,
    DefaultAutocompleteComponent,
  ],
  providers: [
    NumberGenarateSevice,
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' },
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } }
  ]
})
export class SharedModule {
}
