import {createAction, props} from "@ngrx/store";
import {IMaterial} from "../resources/models/material-dto";
import {IMaterialType} from "../resources/models/material-type-dto";
import {IMeasurement} from "../resources/models/measurement-dto";
import {IService} from "../resources/models/service";

export const loadServices = createAction(
  '[Service Component] Load Services'
);
export const loadServicesSuccess = createAction(
  '[Service Component] Services loaded successfully',
  props<{services: IService[]}>()
);

export const loadServicesFailure = createAction(
  '[Service Component] Failed to load services',
  props<{error:any}>()
);
export const loadTypes = createAction(
  '[Service Component] Load types'
);
export const loadTypesSuccessfully = createAction(
  '[Service Component] Types loaded successfully',
  props<{types:string[]}>()
);
export const loadTypesFailure = createAction(
  '[Service Component] Failed to load files',
  props<{error:any}>()
);
export const plusClicked = createAction(
  "[Service Component] Plus was clicked"
);
export const cancelAddClicked = createAction(
  "[Service Component] Cancel of adding was clicked"
);
export const addSubmitted = createAction(
  "[Service Component] Trying to add service",
  props<{service: IService}>()
)
export const addSubmittedSuccessfully = createAction(
  "[Service Component] Add service success",
  props<{service: IService}>()
);
export const addSubmittedFailure = createAction(
  "[Service Component] Add service failure",
  props<{error: any}>()
);
export const editSubmitted = createAction(
  "[Service Component] Trying to edit service",
  props<{service: IService}>()
);
export const editSubmittedSuccessfully = createAction(
  "[Service Component] Edit service success",
  props<{service: IService}>()
);
export const editSubmittedFailure = createAction(
  "[Service Component] Edit service failure",
  props<{error: any}>()
);
export const deleteServiceSubmitted = createAction(
  '[Service Component] Trying to delete service',
  props<{id: number}>()
);
export const deleteServiceSubmittedSuccess = createAction(
  '[Service Component] Delete service success',
  props<{service:number}>()
);
export const deleteServiceSubmittedFailure = createAction(
  '[Service Component] Delete service failure',
  props<{error: any}>()
);
export const serviceInvalid = createAction(
  '[Service Component] Cannot add invalid service'
);
export const getAllServicesWithParams = createAction(
  '[Service Component] Get All Services With Parameters',
  props<Partial<{ filter: string; sort: string }>>()
);
export const getAllServicesWithParamsSuccess = createAction(
  '[Service Component] Services With Parameters got successfully',
  props<{services: IService[]}>()
);
export const getAllServicesWithParamsFailure = createAction(
  '[Service Component] Services With Parameters was not got',
  props<{error: any}>()
);
export const addClickFailure = createAction(
  '[Service Component] Plus click failed',
  props<{message: string}>()
);
export const addMaterialClickFailure = createAction(
  '[Material Component] Plus click failed',
  props<{message: string}>()
);
export const getMaterials = createAction(
  '[Material Component] Load Material',
);
export const getMaterialsWithSuccess = createAction(
  '[Material Component] Material loaded successfully',
  props<{ materials: IMaterial[], total: number }>()
);

export const getMaterialFailure = createAction(
  '[Material Component] Failed to load materials',
  props<{ error: any }>()
);
export const loadMaterialTypes = createAction(
  '[Material types Component] Load types'
);

export const loadMaterialTypesSuccessfully = createAction(
  '[Material Component] Material Types loaded successfully',
  props<{materialTypes:IMaterialType[]}>()
);

export const loadMaterialTypesFailure = createAction(
  '[Material Component] Failed to load files',
  props<{ error: any }>()
);
export const loadMeasurement = createAction(
  '[Material Component] Load measurement '
);
export const loadMeasurementSuccessfully = createAction(
  '[Material Component] Load measurement successfully',
  props<{ measurement: IMeasurement[] }>()
);
export const loadMeasurementFailure = createAction(
  '[Material Component] Failed to load measurement',
  props<{ error: any }>()
);
export const deleteMaterialSubmitted = createAction(
  '[Material Component] Trying to delete material',
  props<{ id: number }>()
);
export const deleteMaterialSubmittedSuccess = createAction(
  '[Material Component] Delete material success',
  props<{ material: number }>()
);
export const deleteMaterialSubmittedFailure = createAction(
  '[Material Component] Delete material failure',
  props<{ error: any }>()
);
export const cancelMaterialAddClicked = createAction(
  "[Material Component] Cancel of adding was clicked"
);
export const plusMaterialClicked = createAction(
  "[Material Component] Plus was clicked"
);
export const addMaterialSubmitted = createAction(
  "[Material Component] Trying to add material",
  props<{material: IMaterial}>()
)
export const addMaterialSubmittedSuccessfully = createAction(
  "[Service Component] Add material success",
  props<{material: IMaterial}>()
);
export const addMaterialSubmittedFailure = createAction(
  "[Material Component] Add material failure",
  props<{error: any}>()
);
export const editMaterialSubmitted = createAction(
  "[Material Component] Trying to edit material",
  props<{material: IMaterial}>()
);
export const editMaterialSubmittedSuccessfully = createAction(
  "[Material Component] Edit material success",
  props<{material: IMaterial}>()
);
export const editMaterialSubmittedFailure = createAction(
  "[Material Component] Edit material failure",
  props<{error: any}>()
);
