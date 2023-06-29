import {
  createFormGroupState,
  updateGroup,
  validate,
} from 'ngrx-forms';
import { required, greaterThanOrEqualTo } from 'ngrx-forms/validation';

export interface ProjectInformationFormValue {
  address: string;
  startTime: string;
  endTime: string
}

export const FORM_ID = 'administration project information form';

export const initialFormState =
  createFormGroupState<ProjectInformationFormValue>(FORM_ID, {
    address: '',
    startTime: '',
    endTime: ''
  });

export const validateCompanyInformationForm =
  updateGroup<ProjectInformationFormValue>({
    address: validate(required),
    startTime: validate(required),
    endTime: validate(required),
  });
