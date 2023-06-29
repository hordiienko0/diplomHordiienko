import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {Observable, EMPTY, of} from 'rxjs';
import * as AdministrationActions from './administration.actions';
import * as DialogActions from '../../../store/actions/modal-dialog.action'
import * as AuthorizationActions from '../../../store/actions/auth.actions';
import {
  catchError,
  map,
  concatMap,
  mergeMap,
  withLatestFrom,
  switchMap, tap,
} from 'rxjs/operators';
import {serializeError} from 'serialize-error';

import {AdministrationApiService} from '../resources/services/administration-api.service';
import {select, Store} from '@ngrx/store';
import {AppState} from 'src/app/store';
import * as AdministrationSelectors from './administration.selectors';
import {ICompanyUpdate} from '../resources/models/company-update.model';
import {AddMemberFailureComponent} from "../add-member-failure/add-member-failure.component";
import {AdministrationFileService} from "../resources/services/administration-file.service";
import {selectCompanyId} from "./administration.selectors";
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationService } from 'src/app/shared/services/notification.service';
@Injectable()
export class AdministrationEffects {
  getAllCompaniesWithParams$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AdministrationActions.getAllCompaniesWithParams),
      concatMap((action) =>
        this.service
          .getAllCompaniesWithParameters(action.filter as string, action.sort as string)
          .pipe(
            map((companies) =>
              AdministrationActions.getAllCompaniesWithParamsSuccess({
                data: companies,
              })
            ),
            catchError((error) =>
              of(
                AdministrationActions.getAllCompaniesWithParamsFailure({
                  error: serializeError(error),
                })
              )
            )
          )
      )
    );
  });

  loadRoles$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AdministrationActions.loadRoles),
      mergeMap((action) =>
        this.service.getAllRoles().pipe(
          map((data) =>
            AdministrationActions.loadRolesSuccess({roles: data})
          ),
          catchError((error) =>
            of(AdministrationActions.loadRolesFailure({error: serializeError(error)}))
          )
        )
      )
    )
  });

  loadAdministrations$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AdministrationActions.loadAdministrations),
      concatMap(() =>
        /** An EMPTY observable only emits completion. Replace with your own observable API request */
        EMPTY.pipe(
          map((data) =>
            AdministrationActions.loadAdministrationsSuccess({data})
          ),
          catchError((error) =>
            of(AdministrationActions.loadAdministrationsFailure({error}))
          )
        )
      )
    );
  });

  $addNewMember = createEffect(() => {
    return this.actions$.pipe(
      ofType(AdministrationActions.addNewMember),
      mergeMap((action) =>
        this.service.postMember(action.data).pipe(
          map((result) =>
            AdministrationActions.addNewMemberSuccess({
              data: {
                ...action.data,
              },
            })
          ),
          catchError((error) =>
            of(
              AdministrationActions.addNewMemberFailure({
                error: serializeError(error),
              })
            )
          )
        )
      )
    );
  });

  createCompany$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AdministrationActions.CreateCompany),
      mergeMap((action) =>
        this.service.createCompany(action.date).pipe(
          map((response) => AdministrationActions.CreateCompanySuccess()),
          catchError((error) =>
            of(
              AdministrationActions.CreateCompanyFailure({
                error: serializeError(error),
              })
            )
          )
        )
      )
    );
  });

  loadOpenCompany = createEffect(() => {
    return this.actions$.pipe(
      ofType(AdministrationActions.loadDetailedCompany),
      mergeMap((action) =>
        this.service.getDetailedCompany(action.id).pipe(
          map((data) =>
            AdministrationActions.loadDetailedCompanySuccess({
              result: {...data, members: []},
            })
          ),
          catchError((error) =>
            of(
              AdministrationActions.loadDetailedCompanyFailure({
                error: serializeError(error),
              })
            )
          )
        )
      )
    );
  });

  getCompanyProjects$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AdministrationActions.getCompanyProjects),
      withLatestFrom(this.store.pipe(select(AdministrationSelectors.selectCompanyProjectsParams))),
      mergeMap(([props, params]) =>
        this.service.getCompanyProjects(props.id, params).pipe(
          map((data) =>
            AdministrationActions.getCompanyProjectsSuccess({
              projects: data.list,
              total: data.total
            })
          ),
          catchError((error) =>
            of(
              AdministrationActions.getCompanyProjectsFailure({
                error: serializeError(error),
              })
            )
          )
        )
      )
    );
  });

  uploadImage = createEffect(() => {
    return this.actions$.pipe(
      ofType(AdministrationActions.UploadCompanyImage),
      mergeMap((action) =>
        this.service.postCompanyImage(action.id, action.image).pipe(
          map((path) =>
            AdministrationActions.UploadCompanyImageSuccess({path: path})
          ),
          catchError((error) =>
            of(AdministrationActions.UploadCompanyImageFailure(error))
          )
        )
      )
    );
  });
  updateCompanyInformationFormState = createEffect(() => {
    return this.actions$.pipe(
      ofType(AdministrationActions.loadDetailedCompanySuccess),
      switchMap((action) => [
        AdministrationActions.loadDisabledCompanyInformationForm(),
        AdministrationActions.loadMembersToOpenCompany({
          companyId: action.result.id,
        }),
      ])
    );
  });
  cancelCompanyInformationFormState = createEffect(() => {
    return this.actions$.pipe(
      ofType(AdministrationActions.cancelEditCompanyInformationForm),
      map(() => AdministrationActions.loadDisabledCompanyInformationForm())
    );
  });
  submitCompanyInformationFormState = createEffect(() => {
    return this.actions$.pipe(
      ofType(AdministrationActions.submitCompanyInformationForm),
      withLatestFrom(
        this.store.select(AdministrationSelectors.selectCurrentlyOpenCompany)
      ),
      switchMap(([action, company]) => {
        return this.service
          .putDetailedCompany({
            id: company.id,
            country: company.country,
            city: company.city,
            companyName: company.companyName,
            email: action.email,
            address: action.address,
          } as ICompanyUpdate)
          .pipe(
            map((result) =>
              AdministrationActions.loadDetailedCompany({id: result.id})
            ),
            catchError((error) =>
              of(
                AdministrationActions.submitCompanyInformationFormFailure({
                  error: serializeError(error),
                })
              )
            )
          );
      })
    );
  });
  submitCompanyInformationFormFailureEffects = createEffect(() => {
    return this.actions$.pipe(
      ofType(AdministrationActions.submitCompanyInformationFormFailure),
      map(() => AdministrationActions.loadDisabledCompanyInformationForm())
    );
  });
  loadMembersToCurrentlyOpenCompany$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AdministrationActions.loadMembersToOpenCompany),
      mergeMap((action) =>
        this.service.getMembersByCompanyId(action.companyId).pipe(
          map((result) =>
            AdministrationActions.loadMembersToOpenCompanySuccess({
              result: result,
            })
          ),
          catchError((error) =>
            of(
              AdministrationActions.loadMembersToOpenCompanySuccessFailure({
                error: serializeError(error),
              })
            )
          )
        )
      )
    );
  });
  openErrorDialog$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AdministrationActions.uploadFileSuccess),
      map((action) => {
        if (action.errorLines!.length != 0)
          return DialogActions.openModalDialog({component: AddMemberFailureComponent})
        else
          return AdministrationActions.uploadFileSuccessWithoutError()
      }),
      catchError((error) =>
        of(
          AdministrationActions.uploadFileFailure({
            error: serializeError(error),
          })
        )
      )
    );
  });
  loadNewMembers$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AdministrationActions.uploadFileSuccess),
      map((action)=>{
        return AdministrationActions.loadMembersToOpenCompany({companyId:action.companyId})
      })
    )
  });


  initStart$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AdministrationActions.GetUserDetailsSuccess),
      tap(() =>
        this.notifService.startService()
      )
    ),
    { dispatch: false });

  getUserDetails$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AuthorizationActions.loginSuccess,
        AuthorizationActions.refreshAccessTokenSuccess),
      mergeMap(({ user: { id } }) =>
        this.service.getUserDetails(id).pipe(
          map(userDetails => AdministrationActions.GetUserDetailsSuccess({ userDetails })),
          catchError((error: HttpErrorResponse) =>
            of(AdministrationActions.GetUserDetailsFailure({ error: error.error }))))
      ),
    );
  });

  updateNewCompanyId$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AdministrationActions.updateNewCompanyId),
      mergeMap((action) =>
        this.service.getNewGeneratedCompanyId().pipe(
          map((id) =>
            AdministrationActions.updateNewCompanyIdSuccess({ newCompanyId: id })
          ), catchError((err) => of(AdministrationActions.updateNewCompanyIdFailure))
        )
      )

    )

  })

  constructor(
    private actions$: Actions,
    private service: AdministrationApiService,
    private fileService: AdministrationFileService,
    private store: Store<AppState>,
    private notifService: NotificationService
  ) {
  }
}
