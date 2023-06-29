import {Injectable} from "@angular/core";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import * as ResourceAction from "./manage-resources.actions"
import {catchError, concatMap, map, switchMap, withLatestFrom} from "rxjs/operators";
import {mergeMap, of} from "rxjs";
import {ManageResourcesApiService} from "../resources/services/manage-resources-api.service";
import {AppState} from "../../../store";
import {select, Store} from "@ngrx/store";
import * as MaterialSelector from "./manage-resources.selectors"
import {serializeError} from "serialize-error";
import {selectUserId} from "../../../store/selectors/auth.selectors";
import {selectServicesParams} from "./manage-resources.selectors";

@Injectable()
export class ManageResourcesEffects {
  getMaterials$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ResourceAction.getMaterials),
      withLatestFrom(this.store.pipe(select(MaterialSelector.selectMaterialsByParams))),
      mergeMap(([props, params]) =>
        this.services.getMaterial(params).pipe(
          map((data) =>
              ResourceAction.getMaterialsWithSuccess({
              materials: data.list,
              total: data.total
              })
            ),
            catchError((error) =>
              of(
                ResourceAction.getMaterialFailure({
                  error: serializeError(error),
                })
              )
            )
          )
      )
    );
  });
  loadMaterialTypes$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ResourceAction.loadMaterialTypes),
      mergeMap(() =>
        this.services.getMaterialType().pipe(
          map((types) =>
            ResourceAction.loadMaterialTypesSuccessfully({materialTypes: types})
          ),
          catchError((error) =>
            of(ResourceAction.loadMaterialTypesFailure(error)))
          )
        )
      )
  });

  loadMeasurement$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ResourceAction.loadMeasurement),
      mergeMap((action) =>
        this.services.getMeasurement().pipe(
          map((data) =>
            ResourceAction.loadMeasurementSuccessfully({measurement: data})
          ),
          catchError((error) =>
            of(ResourceAction.loadMeasurementFailure({error: serializeError(error)})))
        ))
    )
  });

  deleteMaterial$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ResourceAction.deleteMaterialSubmitted),
      mergeMap((action) =>
        this.services.deleteMaterial(action.id).pipe(
          map(() => ResourceAction.getMaterials()),
          catchError((error) => of(ResourceAction.deleteMaterialSubmittedFailure({error: serializeError(error)})
          ))
        ))
    )
  });
  createMaterial$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ResourceAction.addMaterialSubmitted),
      mergeMap((action) =>
        this.services.createMaterial(action.material).pipe(
          map(() =>
              ResourceAction.getMaterials(),
            catchError((error) => of(ResourceAction.addSubmittedFailure({error: serializeError(error)})
          ),
            )
          )
        )
      )
    )
  });
  editMaterial$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ResourceAction.editMaterialSubmitted),
      mergeMap((action) =>
        this.services.editMaterial(action.material).pipe(
          map(() =>
              ResourceAction.getMaterials(),
            catchError((error) => of(ResourceAction.editSubmittedFailure({error: serializeError(error)})
          ),
            )
          )
        )
      )
    )
  });
  getAllServicesWithParams$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ResourceAction.getAllServicesWithParams),
      withLatestFrom(this.store.pipe(select(selectServicesParams))),
      switchMap(([_, params]) =>
        this.services
          .getAllServicesWithParameters(params.filter, params.sort)
          .pipe(
            map((services) =>
              ResourceAction.getAllServicesWithParamsSuccess({
                services: services,
              })
            ),
            catchError((error) =>
              of(
                ResourceAction.getAllServicesWithParamsFailure({
                  error: serializeError(error),
                })
              )
            )
          )
      )
    );
  });
  loadServices$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ResourceAction.loadServices),
      withLatestFrom(this.store.pipe(select(selectUserId))),
      mergeMap(([_,userId]) =>
        this.services.loadServices(userId!).pipe(
          map((data) => {
              return ResourceAction.loadServicesSuccess({services: data});
            }
          ),
          catchError((error) =>
            of(ResourceAction.loadServicesFailure({error: serializeError(error)}))
          )
        )
      )
    );
  });

  addService$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ResourceAction.addSubmitted),
      mergeMap((action) =>
        this.services.addService(action.service).pipe(
          map((service) =>
            ResourceAction.addSubmittedSuccessfully({service: service})
          ),
          catchError(
            (error) =>
              of(
                ResourceAction.addSubmittedFailure({
                  error: serializeError(error),
                })
              )
          )
        )
      )
    )
  });

  editService$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ResourceAction.editSubmitted),
      mergeMap((action) =>
        this.services.editService(action.service).pipe(
          map((service) =>
            ResourceAction.editSubmittedSuccessfully({service: service})
          ),
          catchError(
            (error) =>
              of(
                ResourceAction.editSubmittedFailure({
                  error: serializeError(error),
                })
              )
          )
        )
      )
    )
  });

  deleteService$ = createEffect(()=>{
    return this.actions$.pipe(
      ofType(ResourceAction.deleteServiceSubmitted),
      mergeMap((action)=>
        this.services.deleteService(action.id).pipe(
          map((id)=>
            ResourceAction.deleteServiceSubmittedSuccess({service:id})
          ),
          catchError((error) =>
            of(
              ResourceAction.editSubmittedFailure({
                error: serializeError(error),
              })
            )
          )
        )
      )
    )
  });

  loadTypes$ = createEffect(()=>{
    return this.actions$.pipe(
      ofType(ResourceAction.loadTypes),
      mergeMap((action)=>
        this.services.loadTypes().pipe(
          map((types)=>
            ResourceAction.loadTypesSuccessfully({types:types})
          ),
          catchError((error)=>
            of(
              ResourceAction.editSubmittedFailure({
                error: serializeError(error),
              })
            )
          )
        )
      )
    )
  });
  constructor(private actions$: Actions,
              private store: Store<AppState>,
              private services: ManageResourcesApiService) {
  }
}

