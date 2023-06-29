import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import * as fromMyEntity from './state/my-entity.reducer';
import { EffectsModule } from '@ngrx/effects';
import { MyEntityEffects } from './state/my-entity.effects';
import { MyEntityComponent } from './my-entity.component';
import { MyEntityDetailComponent } from './my-entity-detail/my-entity-detail.component';
import { HttpClientModule } from '@angular/common/http';
import { MyEntityService } from './resources/services/my-entity-api.service';
import {SharedModule} from "../../shared/shared.module";

@NgModule({
  declarations: [
    MyEntityComponent,
    MyEntityDetailComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    HttpClientModule,
    StoreModule.forFeature(fromMyEntity.myEntityFeatureKey, fromMyEntity.reducer),
    EffectsModule.forFeature([MyEntityEffects]),
  ],
  exports: [
    MyEntityComponent
  ],
  providers: [
    MyEntityService
  ]
})
export class MyEntityModule {}
