import { createAction, props } from '@ngrx/store';
import { ICompanyLogo } from '../recources/models/company-logo';
import { ICompanyLogoId } from '../recources/models/company-logo-id';
import {ICompanyProfile} from "../recources/models/company-profile";
import {IProjectOverview} from "../recources/models/project-overview";

export const loadCompany = createAction(
  '[Company Component] Load Company',
);

export const loadCompanySuccess = createAction(
  '[Company Api Service] Load Company Success',
  props<{ company: ICompanyProfile }>()
);

export const loadCompanyFailure = createAction(
  '[Company Api Service] Load Company Failure',
  props<{ error: any }>()
);

export const loadProjects = createAction(
  '[Company Component] Load projects',
);

export const loadProjectsSuccess = createAction(
  '[Company Api Service] Load Projects Success',
  props<{ projects: IProjectOverview[] }>()
);

export const loadProjectsFailure = createAction(
  '[Company Api Service] Load Projects Failure',
  props<{ error: any }>()
);

export const loadDisabledCompanyProfileForm = createAction(
  '[Company Component] Load Disabled Company Profile Form'
);

export const enableEditingCompanyProfileForm = createAction(
  '[Company Component] Enable Editing Company Profile Form'
);

export const cancelEditingCompanyProfileForm = createAction(
  "[Company Component] Cancel Editing Company Profile Form"
);

export const submitEditingCompanyProfileForm = createAction(
  "[Company Component] Submit Editing Company Profile Form",
  props<{ address: string; email: string; website: string }>()
);

export const submitEditingCompanyProfileFormSuccess = createAction(
  "[Company Effect] Submit Editing Company Profile Form Success",
  props<{result: ICompanyProfile}>()
);

export const submitEditingCompanyProfileFormFailure = createAction(
  "[Company Effect] Submit Editing Company Profile Form Failure",
  props<{error : any}>()
);

export const loadCompanyLogo = createAction(
  '[Company Component] Load Company Logo',
);

export const loadCompanyLogoSuccess = createAction(
  '[Company Service] Load Company Logo Success',
  props<{logo : ICompanyLogo}>()
);

export const loadCompanyLogoFailure = createAction(
  '[Company Service] Load Company Logo Failure',
  props<{error : any}>()
);

export const deleteCompanyLogo = createAction(
  '[Company Component] Delete Company Logo',
)

export const deleteCompanyLogoSuccess = createAction(
  '[Company Service] Delete Company Logo Success',
  props<{logo : ICompanyLogoId}>()
);

export const deleteCompanyLogoFailure = createAction(
  '[Company Service] Delete Company Logo Failure',
  props<{error : any}>()
);

export const loadCropImageStart = createAction(
  '[Company Image Crop Component] Load Image to Crop Start',
);

export const loadCropImageFinish = createAction(
  '[Company Image Crop Component] Load Image to Crop Finish',
);

export const loadCropImageFailed = createAction(
  '[Company Image Crop Component] Load Image to Crop Failed',
);


export const uploadCompanyLogoSuccess = createAction(
  '[Company Component] Upload Company Logo Success'
);

export const uploadCompanyLogoFailure = createAction(
  '[Company Component] Upload Company Logo Failure',
);
