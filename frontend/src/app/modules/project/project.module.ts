import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ProjectComponent} from './project.component';
import {ProjectRoutingModule} from './project-routing.module';
import {StoreModule} from '@ngrx/store';
import * as fromProject from './state/project.reducer'
import {ProjectEffects} from './state/project.effects';
import {EffectsModule} from '@ngrx/effects';
import {MatTabsModule} from '@angular/material/tabs';
import {ProjectDashboardComponent} from './project-dashboard/project-dashboard.component';
import {MatIconModule} from '@angular/material/icon';
import {ProjectDescriptionComponent} from './project-description/project-description.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {NgrxFormsModule} from 'ngrx-forms';
import {AddProjectComponent} from './add-project/add-project.component';
import {MatDialogModule} from '@angular/material/dialog';
import {MatMenuModule} from '@angular/material/menu';
import {AddProjectPhotosComponent} from './add-project-photos/add-project-photos.component';
import {ImagePipe} from './resources/pipes/imagePipe';
import {IvyCarouselModule} from 'angular-responsive-carousel2';
import {ProjectPhotosComponent} from './project-photos/project-photos.component';
import {MatButtonModule} from '@angular/material/button';
import {SharedModule} from 'src/app/shared/shared.module';
import {NgxDropzoneModule} from 'ngx-dropzone';
import {MatSelectModule} from '@angular/material/select';
import {TimelineComponent} from './timeline/timeline.component';
import {
  DayMarkersService,
  EditService,
  FilterService,
  GanttModule,
  ReorderService,
  ResizeService,
  SelectionService,
  SortService, ToolbarService
} from "@syncfusion/ej2-angular-gantt";
import {GanttChartComponent} from './gantt-chart/gantt-chart.component';
import { BuildingSectionComponent } from './building-section/building-section.component';
import { BuildingsListItemComponent } from './buildings-list-item/buildings-list-item.component';
import {MatProgressBarModule} from "@angular/material/progress-bar";
import { ProjectDocumentsComponent } from './projectDocuments/project-documents/project-documents.component';
import { ProjectDocumentOverviewComponent } from './projectDocuments/project-document-overview/project-document-overview.component';
import { BuildingAddingListItemComponent } from './building-adding-list-item/building-adding-list-item.component';
import { BuildingManagementDialogComponent } from './building-management-dialog/building-management-dialog.component';
import { AddProjectTeamComponent } from "./add-project-team/add-project-team.component";
import { ClipboardModule } from "@angular/cdk/clipboard";
import { OverlayModule } from "@angular/cdk/overlay";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { PhaseBarComponent } from './phase-bar/phase-bar.component';
import { ProjectDocumentRenameComponent } from './projectDocuments/project-document-rename/project-document-rename.component';
import { AddPhaseDialogComponent } from './add-phase-dialog/add-phase-dialog.component';
import {
  AddBuildingDocumentOverlay
} from "./building-management-dialog/add-building-document-overlay/add-building-document-overlay.component";
import {
    AddBuildingServiceComponent
} from "./building-management-dialog/add-building-service/add-building-service.component";
import { AddBuildingMaterialOverlay } from "./building-management-dialog/add-building-material-overlay/add-building-material-overlay.component"
import { ProjectMaterialsComponent } from './project-materials/project-materials.component';
import { ProjectDocumentViewComponent } from "./projectDocuments/project-document-view/project-document-view.component";

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ProjectRoutingModule,
    StoreModule.forFeature(
      fromProject.projectFeatureKey,
      fromProject.reducer
    ),
    EffectsModule.forFeature([ProjectEffects]),
    MatTabsModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatMenuModule,
    MatButtonModule,
    IvyCarouselModule,
    NgxDropzoneModule,
    MatSelectModule,
    NgrxFormsModule,
    MatProgressBarModule,
    MatCheckboxModule,
    GanttModule,
    MatCheckboxModule,
    ClipboardModule,
    OverlayModule,
  ],
  declarations: [
    ProjectComponent,
    ProjectDashboardComponent,
    ProjectDescriptionComponent,
    AddProjectComponent,
    ProjectPhotosComponent,
    AddProjectPhotosComponent,
    AddProjectTeamComponent,
    ImagePipe,
    TimelineComponent,
    GanttChartComponent,
    BuildingSectionComponent,
    BuildingsListItemComponent,
    BuildingAddingListItemComponent,
    BuildingManagementDialogComponent,
    PhaseBarComponent,
    ProjectDocumentsComponent,
    ProjectDocumentOverviewComponent,
    ProjectDocumentRenameComponent,
    AddBuildingDocumentOverlay,
    AddBuildingMaterialOverlay,
    AddBuildingServiceComponent,
    AddPhaseDialogComponent,
    ProjectMaterialsComponent,
    ProjectDocumentViewComponent,
  ],
  providers: [
    ResizeService,
    SortService,
    FilterService,
    SelectionService,
    ReorderService,
    EditService,
    DayMarkersService,
    ToolbarService
  ],
})
export class ProjectModule {
}
