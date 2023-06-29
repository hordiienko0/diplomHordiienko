import {Params} from "../resources/models/params";
import {IMaterial} from "../resources/models/material-dto";
import {createReducer, on} from "@ngrx/store";
import {onNgrxForms} from "ngrx-forms";
import * as ManageResourceActions from "./manage-resources.actions"
import {ICompanyDetailed} from "../../administration/resources/models/company-detailed.model";
import {IMeasurement} from "../resources/models/measurement-dto";
import {IMaterialType} from "../resources/models/material-type-dto";
import {IService} from "../resources/models/service";

export const manageResourceFeatureKey = "manageResource"

export interface State {
  resourceParams: {
    filter: string,
    sort: string,
  }
  materialParams: Partial<Params>,
  materialsTypes: IMaterialType[],
  measurement: IMeasurement[],
  currentlyOpenCompany: ICompanyDetailed,
  services: IService[],
  types: string[],
  error:any,
  selectedService: IService,
}

export const initialState: State = {
  resourceParams: {
    filter: '',
    sort: '',
  },
  materialParams: {
    sort: "id",
    order: 1
  },
  materialsTypes: [] as IMaterialType[],
  measurement: [] as IMeasurement[],
  currentlyOpenCompany: {
    materials: [] as IMaterial[],
    materialTotalCount: 0
  } as ICompanyDetailed,
  services: [],
  types: [],
  error:null,
  selectedService: {} as IService,
}
export const reducer = createReducer(
  initialState,
  onNgrxForms(),

  on(ManageResourceActions.getMaterialsWithSuccess, (state, action) => ({
    ...state,
    currentlyOpenCompany: {
      ...state.currentlyOpenCompany,
      materials: action.materials,
      materialTotalCount: action.total
    }
  })),

  on(ManageResourceActions.getMaterialFailure, (state, action) => ({
    ...state,
    error: action.error
  })),
  on(
    ManageResourceActions.loadMaterialTypesSuccessfully,
    (state, action) => {
      return {
        ...state,
        materialsTypes: action.materialTypes
      }
    }
  ),
  on(
    ManageResourceActions.loadMeasurementSuccessfully,
    (state, action) => {
      return {
        ...state,
        measurement: action.measurement
      }
    }
  ),
  on(
    ManageResourceActions.deleteMaterialSubmittedSuccess,
    (state, action)=>{
      let index = state.currentlyOpenCompany.materials.findIndex(s=>s.id==action.material);
      let arr = [...state.currentlyOpenCompany.materials];
      arr.splice(index,1);
      return{
        ...state,
        materials:arr
      }
    }
  ),
  on(
    ManageResourceActions.deleteServiceSubmittedSuccess,
    (state, action)=>{
      let index = state.services.findIndex(s=>s.id==action.service);
      let arr = [...state.services];
      arr.splice(index,1);
      return{
        ...state,
        services:arr
      }
    }
  ),
  on(
    ManageResourceActions.cancelAddClicked,
    (state) => {
      return {
        ...state,
        materials: state.currentlyOpenCompany.materials.slice(1)
      }
    }
  ),
  on(
    ManageResourceActions.plusMaterialClicked,
    (state) => {
      return {
        ...state,
        currentlyOpenCompany: {
          ...state.currentlyOpenCompany,
          materials: [{} as IMaterial, ...state.currentlyOpenCompany.materials]
        }
      }
    }
  ),
  on(
    ManageResourceActions.editMaterialSubmittedSuccessfully,
    (state, action) => {
      let arr = [action.material, ...state.currentlyOpenCompany.materials.slice(1)]
      return {
        ...state,
        materials: arr
      }
    }
  ),
  on(
    ManageResourceActions.editMaterialSubmitted,
    (state, action) => {
      let index = state.currentlyOpenCompany.materials.findIndex(s => s.id == action.material.id);
      let arr = [...state.currentlyOpenCompany.materials];
      arr[index] = action.material;
      return {
        ...state,
        materials: arr
      }
    }
  ), on(
    ManageResourceActions.loadServicesSuccess,
    (state, action) => {
      return {
        ...state,
        services: action.services,
      };
    }
  ),
  on(
    ManageResourceActions.plusClicked,
    (state)=>{
      return{
        ...state,
        services: [{} as IService,...state.services]
      }
    }
  ),
  on(
    ManageResourceActions.addSubmittedSuccessfully,
    (state, action)=>{
      let arr = [action.service,...state.services.slice(1)]
      return{
        ...state,
        services: arr
      }
    }
  ),
  on(
    ManageResourceActions.editSubmittedSuccessfully,
    (state,action)=>{
      let index = state.services.findIndex(s => s.id == action.service.id);
      let arr = [...state.services];
      arr[index]=action.service;
      return{
        ...state,
        services: arr
      }
    }
  ),
  on(
    ManageResourceActions.cancelAddClicked,
    (state)=>{
      return{
        ...state,
        services: state.services.slice(1)
      }
    }
  ),

  on(
    ManageResourceActions.loadTypesSuccessfully,
    (state, action)=>{
      return{
        ...state,
        types: action.types
      }
    }
  ),
  on(
    ManageResourceActions.getAllServicesWithParams,
    (state, action) => {
      return {
        ...state,
        resourceParams: {
          filter: action.filter ?? state.resourceParams.filter,
          sort: action.sort ?? state.resourceParams.sort,
        },
        error: null,
      };
    }
  ),
  on(
    ManageResourceActions.getAllServicesWithParamsSuccess,
    (state, action) => {
      return {
        ...state,
        services: action.services,
        error: null,
      };
    }
  ),
  on(
    ManageResourceActions.getAllServicesWithParamsFailure,
    (state, action) => {
      return{
        ...state,
        error: action.error
      }
    }
  )
);
