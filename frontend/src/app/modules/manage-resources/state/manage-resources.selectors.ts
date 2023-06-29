import {createFeatureSelector, createSelector} from "@ngrx/store";
import * as fromMaterials from './manage-resources.reducer'

export const selectResourceState =
  createFeatureSelector<fromMaterials.State>(
    fromMaterials.manageResourceFeatureKey
);
export const selectMaterials = createSelector(
  selectResourceState,
  (state) => state.currentlyOpenCompany.materials
)
export const selectMaterialsByParams = createSelector(
  selectResourceState,
  (state)=>state.materialParams
)
export const selectMaterialTypes = createSelector(
  selectResourceState,
  (state) => state.materialsTypes
)
export const selectMeasurement = createSelector(
  selectResourceState,
  (state) => state.measurement
)

export const selectServices = createSelector(
  selectResourceState,
  (state)=>state.services
);

export const selectTypes = createSelector(
  selectResourceState,
  (state)=>state.types
);
export const selectServicesParams = createSelector(
  selectResourceState,
  (state)=>state.resourceParams
)
