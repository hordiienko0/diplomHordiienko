import { createAction, props } from '@ngrx/store';
import {IMenuLink} from "../../core/shell/menu/resources/IMenuLink";

export const openMenu = createAction(
  '[Menu] Open Menu'
);

export const closeMenu = createAction(
  '[Menu] Close Menu'
);

export const hideMenu = createAction(
  '[Menu] Hide Menu'
);

export const revealMenu = createAction(
  '[Menu] Reveal Menu'
);

export const setMenuLinks = createAction(
  '[Menu] Set Menu Links',
  props<{links: IMenuLink[]}>()
);



