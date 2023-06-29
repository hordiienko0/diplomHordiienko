import { createAction, props } from "@ngrx/store";
import { NavigationExtras } from "@angular/router";

export const goBack = createAction('[Routing] Go Back');

export const navigate = createAction(
  '[Routing] Navigate',
  props<{ commands: any[], extras?: NavigationExtras }>()
);
