import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import * as fromAuthActions from '../actions/auth.actions';
import * as fromAdministrationActions from '../../modules/administration/state/administration.actions'
import {tap} from 'rxjs/operators';
import {AlertService} from 'src/app/modules/alert/resources/services/alert.service';
import {ErrorService} from 'src/app/modules/error/resources/services/error.services';
import * as fromProjectActions from '../../modules/project/state/project.actions';
import * as fromCompanyActions from '../../modules/company/state/company.actions';
import {showCustomAlert} from "../actions/alert.actions";
import * as fromServiceActions from '../../modules/manage-resources/state/manage-resources.actions';

@Injectable()
export class AlertEffects {
  loginSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromAuthActions.loginSuccess),
        tap(() => {
            this._alertService.showAlert("Успішна авторизація", "OK", "success")
          }
        )
      ),
    {dispatch: false}
  );

  loginFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromAuthActions.loginFailure),
        tap(() => {
            this._alertService.showAlert("Помилка входу", "OK", "error")
          }
        )
      ),
    {dispatch: false}
  );

  changeDefaultPasswordSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromAuthActions.changeDefaultPasswordSuccess),
        tap(() => {
            this._alertService.showAlert("Password successfully changed", "OK", "success")
          }
        )
      ),
    {dispatch: false}
  );

  changeDefaultPasswordFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromAuthActions.changeDefaultPasswordFailure),
        tap(() => {
            this._alertService.showAlert("Failed to change password", "OK", "error")
          }
        )
      ),
    {dispatch: false}
  );

  addMemberSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromAdministrationActions.addNewMemberSuccess),
        tap(() => {
            this._alertService.showAlert("Member was added successfully", "OK", "success")
          }
        )
      ),
    {dispatch: false}
  );

  addMemberFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromAdministrationActions.addNewMemberFailure),
        tap((action) => {
            this._alertService.showAlert(`Failed to add member. ${action.error.error.detail}`, "OK", "error")
          }
        )
      ),
    {dispatch: false}
  );

  resetPasswordError$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromAuthActions.forgotPasswordFailure),
        tap((action) => {
            this._alertService.showAlert(this.errorService.getErrorMessage(action.error, "Forgot Password"), "OK", "error")
          }
        )
      ),
    {dispatch: false}
  );
  resetPasswordSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromAuthActions.forgotPasswordSuccess),
        tap(() => {
            this._alertService.showAlert("Reset password success", "OK", "success")
          }
        )
      ),
    {dispatch: false}
  );


  companiesListLoadingFailed$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromAdministrationActions.getAllCompaniesWithParamsFailure),
        tap(() => {
          this._alertService.showAlert("Failed load companies from server", "OK", "error")
        })
      ),
    {dispatch: false});
  addMembersSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromAdministrationActions.uploadFileSuccess),
        tap(() => {
            this._alertService.showAlert("Not all users were added", "OK", "warning");
          }
        )
      ),
    {dispatch: false}
  );
  addMembersFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromAdministrationActions.uploadFileFailure),
        tap((action) => {
            this._alertService.showAlert(`Failed to add members. ${action.error.error.detail}`, "OK", "error")
          }
        )
      ),
    {dispatch: false}
  );
  addMembersSuccessWithoutError$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromAdministrationActions.uploadFileSuccessWithoutError),
        tap((action) => {
            this._alertService.showAlert("Members added successfully", "OK", "success")
          }
        )
      ),
    {dispatch: false}
  );
  projectPhotoUploadSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromProjectActions.uploadProjecPhotoSuccess),
        tap(() => {
          this._alertService.showAlert("Upload project photos success", "OK", "success")
        })
      ),
    {dispatch: false});

  projectPhotoUploadFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromProjectActions.uploadProjecPhotoFailure),
        tap(() => {
          this._alertService.showAlert("Upload project photos failure", "OK", "error")
        })
      ),
    {dispatch: false});


  getUserDetailsFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromAdministrationActions.GetUserDetailsFailure),
        tap(({error}) => {
          this._alertService.showAlert(this.errorService.getErrorMessage(error, "Details didnt load"), "OK", "error")
        })
      ),
    {dispatch: false});

  companyProfileEditingSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(fromCompanyActions.submitEditingCompanyProfileFormSuccess),
      tap(() => {
        this._alertService.showAlert("Company profile changed successfully", "OK", "success");
      })
    ), {dispatch: false});

  companyProfileEditingFailure = createEffect(() =>
      this.actions$.pipe(
        ofType(fromCompanyActions.submitEditingCompanyProfileFormFailure),
        tap(({error}) =>
          this._alertService.showAlert("Failed to edit company profile. Check your inputs.", "OK", "error")
        )
      ),
    {dispatch: false});

  uploadCompanyLogoSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromCompanyActions.uploadCompanyLogoSuccess),
        tap(() => {
          this._alertService.showAlert("Company logo upload success", "OK", "success")
        })
      ),
    {dispatch: false});

  uploadCompanyLogoFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromCompanyActions.uploadCompanyLogoFailure),
        tap(() => {
          this._alertService.showAlert("Company logo upload failure", "OK", "error")
        })
      ),
    {dispatch: false});

  deleteProjectDocumentFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromProjectActions.deleteProjectDocumentFailure),
        tap(() => {
          this._alertService.showAlert("Project document delete failure", "OK", "error")
        })
      ),
    {dispatch: false});

  uploadProjectDocumentsFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromProjectActions.uploadProjectDocumentsFailure),
        tap(() => {
          this._alertService.showAlert("Project document upload failure", "OK", "error")
        })
      ),
    {dispatch: false});


  addServiceSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromServiceActions.addSubmittedSuccessfully),
        tap(() => {
          this._alertService.showAlert("Service was added", "OK", "success")
        })
      ),
    {dispatch: false});

  editServiceSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromServiceActions.editSubmittedSuccessfully),
        tap(() => {
          this._alertService.showAlert("Service was edited", "OK", "success")
        })
      ),
    {dispatch: false});
  deleteServiceSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromServiceActions.deleteServiceSubmittedSuccess),
        tap(() => {
          this._alertService.showAlert("Service was deleted", "OK", "success")
        })
      ),
    {dispatch: false});
  loadServiceFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromServiceActions.loadServicesFailure),
        tap(() => {
          this._alertService.showAlert("Error. Cannot load services", "OK", "error")
        })
      ),
    {dispatch: false}
  )
  invalidService$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromServiceActions.serviceInvalid),
        tap(() => {
          this._alertService.showAlert("Error. Cannot add invalid services", "OK", "error")
        })
      ),
    {dispatch: false}
  )
  addClickFailed = createEffect(
    () =>
      this.actions$.pipe(
        ofType(fromServiceActions.addClickFailure),
        tap((action) => {
          this._alertService.showAlert(action.message, "OK", "error")
        })
      ),
    {dispatch: false}
  )


  showCustomAlert$ = createEffect(() =>
    this.actions$.pipe(
      ofType(showCustomAlert),
      tap((action) => {
        this._alertService.showAlert(action.alert.message, action.alert.buttonText, action.alert.type);
      })
    ), {dispatch: false})


        createReportFail = createEffect(
          () =>
            this.actions$.pipe(
              ofType(fromProjectActions.createReportFailure),
              tap(() => {
                this._alertService.showAlert("Failed to create report", "OK", "error")
              })
            ),
          { dispatch: false }
        )
  constructor(private actions$: Actions, private _alertService: AlertService, private errorService: ErrorService) {
  }
}
