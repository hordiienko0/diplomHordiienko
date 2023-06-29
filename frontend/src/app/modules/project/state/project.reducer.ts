import { createReducer, on } from '@ngrx/store';
import { Order } from '../resources/models/order';
import { IProjectOverview } from '../resources/models/project-overview';
import { IProjectPhoto } from '../resources/models/project-photo.model';
import { createFormGroupState, disable, enable, FormGroupState, onNgrxForms, onNgrxFormsAction, SetValueAction } from 'ngrx-forms';
import { ProjectStatus } from '../resources/models/status';
import * as ProjectActions from './project.actions';
import * as fromProjectInformationForm from "../resources/forms/project-information-form"
import { IProjectDetailed } from '../resources/models/project-details';
import {IBuilding} from "../resources/models/building.model";
import { IProjectDocument } from '../resources/models/project-documents/project-document.model';
import {IPhase} from "../resources/models/phase.model";
import { Params } from '../resources/models/params';
import { UsedByProjectMaterial } from '../resources/models/project-material/project-used-material.model';
import { GetRequiredMaterialDtoModel } from "../resources/models/get-required-materials-dto.model";
import { state } from '@angular/animations';
import {IService} from "../resources/models/service";

export const projectFeatureKey = 'project';

export interface State {
  projects: IProjectOverview[],
  params: Params,
  total: number,
  project: IProjectDetailed,
  projectInformationForm: FormGroupState<fromProjectInformationForm.ProjectInformationFormValue>,
  buildings : IBuilding[],
  currentlyRevealedBuilding : number | null,
  currentProject: {
    id: number,
    team: {
      id: number,
      name: string,
      role: string,
      email: string,
      phoneNumber: string
    }[] | null,
  } | null,
  error: string,
  currentlyOpenProjectDocuments: IProjectDocument[]
  currentlyOpenBuildingMaterials: GetRequiredMaterialDtoModel[],
  currentlyOpenBuildingServices: IService[],
  selectedServices: IService[],
  buildingServices: IService[],
  phases: IPhase[],
  curentlyOpenProjectMaterials: UsedByProjectMaterial[]
}

export const initialState: State = {
  projects: [],
  params: {
    page: 1,
    count: 10,
    query: "",
    sort: "Id",
    order: Order.ASC,
    status: ProjectStatus.InProcess
  },
  total: 0,
  error: "",
  project: {} as IProjectDetailed,
  projectInformationForm: fromProjectInformationForm.initialFormState,
  buildings: [],
  currentlyRevealedBuilding: null,
  currentProject: {
    id: 1,
    team: [],
  },
  currentlyOpenProjectDocuments: [],
  phases: [],
  currentlyOpenBuildingMaterials: [],
  currentlyOpenBuildingServices: [],
  curentlyOpenProjectMaterials: [],
  selectedServices: [],
  buildingServices: [],
};

export const reducer = createReducer(
  initialState,
  onNgrxForms(),
  on(ProjectActions.getProjectssWithParamsSuccess, (state, action) => {
    return { ...state, projects: action.data.list, total: action.data.total }
  }),
  on(ProjectActions.changeParams, (state, action) => {
    return { ...state, params: { ...state.params, ...action.params } }
  }),
  on(ProjectActions.getDetailedProjectSuccess, (state, action) => {
    return { ...state, project: {...state.project, ...action.data} }
  }),
  on(ProjectActions.getDetailedProjectFailure, (state, action) => {
      return { ...state, error: action.error };
    }
  ),
  onNgrxFormsAction(SetValueAction, (state, action) => {
    return {
      ...state,
      projectInformationForm:
        fromProjectInformationForm.validateCompanyInformationForm(
          state.projectInformationForm
        ),
    };
  }),
  on(ProjectActions.editProjectInformationForm, (state) => {
    {
      return {
        ...state,
        companyInformationForm: enable(state.projectInformationForm),
      };
    }
  }),
  on(ProjectActions.cancelEditProjectInformationForm, (state) => {
    return {
      ...state,
      companyInformationForm: disable(state.projectInformationForm),
    };
  }),
  on(
    ProjectActions.submitProjectInformationFormFailure,
    (state, action) => {
      return {
        ...state,
        error: action.error,
      };
    }
  ),
  on(
    ProjectActions.loadDisabledProjectInformationForm,
    (state, action) => ({
      ...state,
      projectInformationForm:
        createFormGroupState<fromProjectInformationForm.ProjectInformationFormValue>(
          fromProjectInformationForm.FORM_ID,
          {
            address: state.project.address,
            startTime: state.project.startTime,
            endTime: state.project.endTime,
          }
        )
      ,
    })
  ),
  on(ProjectActions.loadProjectPhotosSuccess, (state, action) => {
    return {
      ...state,
      project: {...state.project, currentlyOpenProjectPhotos: action.data},
    };
  }),
  on(ProjectActions.deleteProjectPhotoSuccess, (state, action) => {
    return {
      ...state,
      project: {...state.project, currentlyOpenProjectPhotos: state.project.currentlyOpenProjectPhotos.filter(
        (p) => p.id != action.id
      ),
    }
  }}),
  on(ProjectActions.getProjectssWithParamsSuccess, (state, action) => {
    return { ...state, projects: action.data.list, total: action.data.total }
  }),
  on(ProjectActions.changeParams, (state, action) => {
    return { ...state, params: { ...state.params, ...action.params } }
  }),
  on(ProjectActions.changeProjectStatusSuccess, (state, action) => {
    return {
      ...state,
      project: {
        ...state.project,
        status: action.newStatus,
      },
    }
  }),
  on(ProjectActions.getProjectTeamSuccess, (state, action) => {
    return {
      ...state,
      currentProject: {
        ...state.currentProject!,
        team: action.response.users.map(u => ({
          id: u.id,
          name: `${u.firstName} ${u.lastName}`,
          role: u.role,
          email: u.email,
          phoneNumber: u.phoneNumber,
        })),
      },
    }
  }),
  on(ProjectActions.getProjectTeamFailure, (state, action) => {
    return {
      ...state,
      currentProject: {
        ...state.currentProject!,
        team: null,
      },
    }
  }),
  on(ProjectActions.loadBuildingWithBuildingBlocksSuccess, (state, action) =>{
    return {
      ...state,
      buildings: action.result
    }
  }),
  on(ProjectActions.loadBuildingWithBuildingBlocksFailure, (state, action) => {
    return {
      ...state,
      error: action.error
    }
  }),
  on(ProjectActions.revealBuildingCard, (state, action) => {
    return {
      ...state,
      currentlyRevealedBuilding: state.currentlyRevealedBuilding == action.id ? null : action.id
    }
  }),
  on(ProjectActions.loadProjectDocumentsSuccess, (state, action)=>{
    return{
    ...state,
    currentlyOpenProjectDocuments: action.response
    }
  }),
  on(ProjectActions.loadProjectDocumentsFailure, (state, action)=>{
    return{
      ...state,
      error: action.error,
      currentlyOpenProjectDocuments: []
    }
  }),
  on(ProjectActions.deleteProjectDocumentSuccess, (state, action)=>{
    return{
    ...state,
    currentlyOpenProjectDocuments: state.currentlyOpenProjectDocuments.filter(document=>document.id != action.response.id)
    }
  }),
  on(ProjectActions.deleteProjectDocumentFailure, (state, action)=>{
    return{
      ...state,
      error: action.error
    }
  }),
  on(ProjectActions.updateProjectDocumentSuccess, (state, action)=>{
    return{
    ...state,
    currentlyOpenProjectDocuments: state.currentlyOpenProjectDocuments.map(obj=>{
      if(obj.id === action.response.id){
        obj = action.response
      }
      return obj;
    }
    )

    }
  }),
  on(ProjectActions.updateProjectDocumentsFailure, (state, action)=>{
    return{
      ...state,
      error: action.error
    }
  }),
  on(ProjectActions.submitCheckedServicesSuccess, (state, action)=>{
    return{
      ...state,
      currentlyOpenBuildingServices: action.services
    }
  }),
  on(ProjectActions.submitCheckedServicesFailure, (state, action)=>{
    return{
      ...state,
      error: action.error
    }
  }),
  on(ProjectActions.loadCheckedServicesSuccess, (state, action)=>{
    return{
      ...state,
      currentlyOpenBuildingServices: action.services
    }
  }),
  on(ProjectActions.loadCheckedServicesFailure, (state, action)=>{
    return{
      ...state,
      error: action.error
    }
  }),
  on(ProjectActions.loadUncheckedBuildingServicesSuccess, (state, action)=>{
    return{
      ...state,
      buildingServices: action.services
    }
  }),
  on(ProjectActions.loadUncheckedBuildingServicesFailure, (state, action)=>{
    return{
      ...state,
      error: action.error
    }
  }),
  on(ProjectActions.loadPhasesForProjectSuccess, (state, action) => {
    return {
      ...state,
      phases: action.phases
    }
  }),
  on(ProjectActions.loadPhasesForProjectFailure, (state, action) => {
    return {
      ...state,
      error: action.error
    }
  }),
  on(ProjectActions.createReportFailure, (state, action)=>{
    return{
      ...state,
      erro:action.error
    }
  }),
  on(ProjectActions.loadUsedForProjectResourcesSuccess, (state, action) => {
    return{
      ...state,
      curentlyOpenProjectMaterials: action.materials
    }
  }),
  on(ProjectActions.loadRequiredMaterialsSuccess, (state, action) => {
    return {
      ...state,
      currentlyOpenBuildingMaterials: action.result.requiredMaterials,
    }
  }),
  on(ProjectActions.loadRequiredMaterialsFailure, (state, action) => {
    return {
      ...state,
      currentlyOpenBuildingMaterials: [],
    }
  }),
  on(ProjectActions.deleteRequiredMaterialSuccess, (state, action) => {
    return{
      ...state,
      currentlyOpenBuildingMaterials: state.currentlyOpenBuildingMaterials.filter(m => m.id != action.requiredMaterialId)
    }
  }),
);
