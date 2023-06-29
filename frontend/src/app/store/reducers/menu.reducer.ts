import { Action, createReducer, on } from '@ngrx/store';
import {IMenuLink} from "../../core/shell/menu/resources/IMenuLink";
import * as fromMenuAction from "../actions/menu.actions";


export const menuFeatureKey = 'menu';

export interface State {
  opened : boolean,
  revealed : boolean,
  links : IMenuLink[],
  logo: string | null
}

export const initialState: State = {
  opened : false,
  revealed : true,
  links: [] as IMenuLink[],
  logo: null,
};

export const reducer = createReducer(
  initialState,
  on(fromMenuAction.openMenu, (state) => {
    return {
      ...state,
      opened: true
    }
  }),
  on(fromMenuAction.closeMenu, (state) => {
    return {
      ...state,
      opened: false
    }
  }),
  on(fromMenuAction.hideMenu, (state) => {
    return {
      ...state,
      revealed: false
    }
  }),
  on(fromMenuAction.revealMenu, (state) => {
    return {
      ...state,
      revealed: true
    }
  }),
  on(fromMenuAction.setMenuLinks, (state, action) => {
    return {
      ...state,
      links: action.links
    }
  }),
);
