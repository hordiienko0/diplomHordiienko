import {createAction, props} from "@ngrx/store";
import {AlertModel} from "../../modules/alert/resources/models/alert-model";


export const showCustomAlert = createAction(
  '[Alert] Show Custom Alert',
  props<{alert : AlertModel}>()
);
