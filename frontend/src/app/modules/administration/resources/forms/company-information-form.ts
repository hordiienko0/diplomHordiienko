import {
  createFormGroupState,
  updateGroup,
  validate,
} from 'ngrx-forms';
import { required, email } from 'ngrx-forms/validation';

export interface CompanyInformationFormValue {
  address: string;
  email: string;
}

export const FORM_ID = 'administration company information form';

export const initialFormState =
  createFormGroupState<CompanyInformationFormValue>(FORM_ID, {
    address: '',
    email: '',
  });

export const validateCompanyInformationForm =
  updateGroup<CompanyInformationFormValue>({
    address: validate(required),
    email: validate(email, required),
  });
