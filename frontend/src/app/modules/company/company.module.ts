import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompanyRoutingModule } from './company-routing.module';
import { CompanyComponent } from './company.component';
import {MatIconModule} from "@angular/material/icon";
import {MatButtonModule} from "@angular/material/button";
import { EffectsModule } from '@ngrx/effects';
import { CompanyEffects } from './state/company.effects';
import {SharedModule} from "../../shared/shared.module";
import {NgrxFormsModule} from "ngrx-forms";
import { CompanyImageCropComponent } from './company-image-crop/company-image-crop.component';
import { ImageCropperModule } from 'ngx-image-cropper';
@NgModule({
  declarations: [
    CompanyComponent,
    CompanyImageCropComponent
  ],
  imports: [
    CommonModule,
    CompanyRoutingModule,
    MatIconModule,
    MatButtonModule,
    EffectsModule.forFeature([CompanyEffects]),
    SharedModule,
    ImageCropperModule,
    NgrxFormsModule
  ]
})
export class CompanyModule { }
