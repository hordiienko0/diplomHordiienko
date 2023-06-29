import { createAction, props } from '@ngrx/store';
import { ICompanyOverview } from '../resources/models/company-overview.model';
import { IMember } from '../resources/models/member.model';
import { INewCompanyDto } from '../resources/DTOmodels/new-company-dto.model';
import { ICompanyDetailed } from '../resources/models/company-detailed.model';
import { IRole } from '../resources/models/role.model';
import { UserDetailsDto } from '../resources/models/userDetailsDto';
import { HttError } from '../../error/resources/models/httpError';
import { ICompanyProject } from '../resources/models/company-project.model';

export const GetUserDetails = createAction(
  '[Administration] Get User Details',
  props<{ userId: number }>()
);
export const GetUserDetailsSuccess = createAction(
  '[Administration] Get User Details Success',
  props<{ userDetails: UserDetailsDto }>()
);
export const GetUserDetailsFailure = createAction(
  '[Administration] Get User Details Failure',
  props<{ error: HttError }>()
);

export const loadAdministrations = createAction(
  '[Administration] Load Administrations'
);

export const loadAdministrationsSuccess = createAction(
  '[Administration] Load Administrations Success',
  props<{ data: any }>()
);

export const loadAdministrationsFailure = createAction(
  '[Administration] Load Administrations Failure',
  props<{ error: any }>()
);

export const getAllCompaniesWithParams = createAction(
  '[Company List Component] Get All Companies With Parameters',
  props<Partial<{ filter: string; sort: string }>>()
);

export const getAllCompaniesWithParamsSuccess = createAction(
  '[Company List Component] Get All Companies With Parameters Success',
  props<{ data: ICompanyOverview[] }>()
);

export const getAllCompaniesWithParamsFailure = createAction(
  '[Company List Component] Get All Companies With Parameters Failure',
  props<{ error: any }>()
);
export const addNewMember = createAction(
  '[Add Company Member Component] Add new member',
  props<{ data: IMember }>()
);
export const addNewMemberSuccess = createAction(
  '[Add Company Member Effect] Add new member success',
  props<{ data: IMember }>()
);
export const addNewMemberFailure = createAction(
  '[Add Company Member Effect] Add new member failure',
  props<{ error: any }>()
);

export const CreateCompany = createAction(
  '[Create Compamy Conponent] Create New Company',
  props<{ date: INewCompanyDto }>()
);

export const CreateCompanySuccess = createAction(
  '[Create Compamy Conponent] Create New Company Success'
);

export const CreateCompanyFailure = createAction(
  '[Create Compamy Conponent] Create New Company Failure',
  props<{ error: any }>()
);

export const loadDetailedCompany = createAction(
  '[Company-information Component] Load Detailed Company',
  props<{ id: number }>()
);

export const loadDetailedCompanySuccess = createAction(
  '[Company-information Component] Load Detailed Company Success',
  props<{ result: ICompanyDetailed }>()
);

export const loadDetailedCompanyFailure = createAction(
  '[Company-information Component] Load Detailed Company Failure',
  props<{ error: any }>()
);

export const UploadCompanyImage = createAction(
  '[Company-information Component] Upload Company Image',
  props<{ id: Number; image: File }>()
);

export const UploadCompanyImageSuccess = createAction(
  '[Company-information Component] Upload Company Image Success',
  props<{ path: string }>()
);

export const UploadCompanyImageFailure = createAction(
  '[Company-information Component]  Upload Company Image Failure',
  props<{ error: any }>()
);

export const loadRoles = createAction(
  '[Add new member component] Loads roles from backend'
);
export const loadRolesSuccess = createAction(
  '[Add new member component] Roles loaded successfully',
  props<{ roles: IRole[] }>()
);
export const loadRolesFailure = createAction(
  '[Add new member component] Failed to load roles',
  props<{ error: any }>()
);

export const getCompanyProjects = createAction(
  '[Company-information Component] Get Company Projects',
  props<{id: number}>()
);
export const getCompanyProjectsSuccess = createAction(
  '[Company-information Component] Get Company Projects Successfully',
  props<{ projects: ICompanyProject[], total: number }>()
);
export const getCompanyProjectsFailure = createAction(
  '[Company-information Component] Failed To Get Company Projects',
  props<{ error: any }>()
);

export const loadDisabledCompanyInformationForm = createAction(
  '[Company-information Component] Load Company Information Form'
);
export const editCompanyInformationForm = createAction(
  '[Company-information Component] Edit Company Information Form'
);

export const submitCompanyInformationForm = createAction(
  '[Company-information Component] Submit Company Information Form',
  props<{ email: string; address: string }>()
);
export const submitCompanyInformationFormSuccess = createAction(
  '[Company-information Component] Submit Company Information Form Success',
  props<{ result: ICompanyDetailed }>()
);
export const submitCompanyInformationFormFailure = createAction(
  '[Company-information Component] Submit Company Information Form Failure',
  props<{ error: any }>()
);
export const cancelEditCompanyInformationForm = createAction(
  '[Company-information Component] Cancel Company Information Form'
);
export const loadMembersToOpenCompany = createAction(
  '[Company-information Component] Load Members To Currently Open Company',
  props<{ companyId: number }>()
);
export const loadMembersToOpenCompanySuccess = createAction(
  '[Company-information Component] Load Members To Currently Open Company Success',
  props<{ result: IMember[] }>()
);
export const loadMembersToOpenCompanySuccessFailure = createAction(
  '[Company-information Component] Load Members To Currently Open Company Failure',
  props<{ error: any }>()
);
export const uploadFile = createAction(
  '[Company-information Component] File to add members sent',
  props<{ companyId: number, file:string }>()
);
export const uploadFileSuccess = createAction(
  '[Company-information Component] Members added successfully',
  props<{ errorLines: string[]|undefined, companyId:number}>()
)
export const uploadFileFailure = createAction(
  '[Company-information Component] Failed to add members',
  props<{ error: any }>()
);
export const uploadFileSuccessWithoutError= createAction(
  '[Company-information Component] Members added successfully without error lines',
)

export const updateNewCompanyId = createAction(
  '[Company List] Update New Company Id'
)

export const updateNewCompanyIdSuccess = createAction(
  '[Company List] Update New Company Id Success',
  props<{ newCompanyId: number }>()
)

export const updateNewCompanyIdFailure = createAction(
  '[Company List] Update New Company Id Failure'
)
