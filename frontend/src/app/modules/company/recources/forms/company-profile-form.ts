import {createFormGroupState} from "ngrx-forms";

export interface CompanyProfileFormValue {
  address : string,
  email : string,
  website : string
}

export const FORM_ID = 'company-profile-form';

export const initialFormState =
  createFormGroupState<CompanyProfileFormValue>(FORM_ID,{
    address: "",
    email: "",
    website : ""
  });


