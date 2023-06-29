import { Component, Inject, OnInit } from '@angular/core';
import { IBuilding } from "../resources/models/building.model";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { FormControl, Validators } from "@angular/forms";
import { select, Store } from "@ngrx/store";
import { AppState } from "../../../store";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { IBuildingBlock } from "../resources/models/building-block.model";
import * as fromModalDialogActions from "../../../store/actions/modal-dialog.action";
import * as fromProjectActions from "../state/project.actions";
import * as fromProjectSelectors from "../state/project.selectors";
import * as fromAuthSelectors from "../../../store/selectors/auth.selectors";
import { UserRole } from "../../auth/resources/models/userRole";
import { MatIconRegistry } from "@angular/material/icon";
import { DomSanitizer } from "@angular/platform-browser";
import { ProjectFileService } from "../resources/services/project-file.services";

interface DialogData {
  building: IBuilding;
}

@Component({
  selector: 'app-building-management-dialog',
  templateUrl: './building-management-dialog.component.html',
  styleUrls: ['./building-management-dialog.component.scss']
})
export class BuildingManagementDialogComponent implements OnInit {

  canEdit$ = this.store.select(fromAuthSelectors.selectUserRole).pipe(
    map(role => role == UserRole.OperationalManager || role == UserRole.ProjectManager)
  );

  isServicesOverlayOpen = false;
  isMaterialsOverlayOpen = false;
  isDocumentsOverlayOpen = false;

  building$: Observable<IBuilding | undefined>
  buildingName: FormControl = new FormControl
  isMouseOverSubsectionListItem: boolean = false;
  isEditing: boolean = false;

  isAdding: boolean = false;
  buildingBlockName: FormControl = new FormControl('', [Validators.required]);

  services$ = this.store.select(fromProjectSelectors.selectBuildingServices);

  singleService$ = this.services$.pipe(
    map(services => this.showAllServices ? services : services.slice(0, 1))
  );

  materials$ = this.store.select(fromProjectSelectors.selectBuildingMaterials);

  singleMaterial$ = this.materials$.pipe(
    map(materials => this.showAllMaterials ? materials : materials.slice(0, 1))
  );

  documents$ = this.store.select(fromProjectSelectors.selectProjectDocuments).pipe(
    map(documents => documents.map(d => {
      return ({ ...d, type: d.fileName.split('.').pop() });
    })),
  );

  singleDocument$ = this.documents$.pipe(
    map(documents => this.showAllDocuments ? documents : documents.slice(0, 1))
  );

  showAllServices = false;
  showAllMaterials = false;
  showAllDocuments = false;

  documentIcons = ['jpg', 'png', 'pdf', 'txt', 'csv', 'doc'];

  constructor(@Inject(MAT_DIALOG_DATA) public data: DialogData,
              private store: Store<AppState>,
              private iconRegistry: MatIconRegistry,
              private sanitizer: DomSanitizer,
              private projectFileService: ProjectFileService) {
    this.building$ = this.store.pipe(
      select(fromProjectSelectors.selectProjectBuildings),
      map(buildings => buildings.find(b => b.id == data.building.id))
    );

    iconRegistry.addSvgIcon(
      'file-csv',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/file-csv.svg')
    );

    iconRegistry.addSvgIcon(
      'file-doc',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/file-doc.svg')
    );

    iconRegistry.addSvgIcon(
      'file-jpg',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/file-jpg.svg')
    );

    iconRegistry.addSvgIcon(
      'file-pdf',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/file-pdf.svg')
    );

    iconRegistry.addSvgIcon(
      'file-png',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/file-png.svg')
    );
  }

  ngOnInit(): void {
    this.loadMaterials();
    this.loadDocuments();
    this.loadServices();
  }

  loadMaterials() {
    this.store.dispatch(fromProjectActions.loadRequiredMaterials({
      buildingId: this.data.building.id
    }));
  }

  loadDocuments() {
    this.store.dispatch(fromProjectActions.loadProjectDocuments({
      projectId: this.data.building.projectId,
      buildingId: this.data.building.id,
      sort: 'created'
    }));
  }

  loadServices() {
    this.store.dispatch(fromProjectActions.loadCheckedServices({buildingId: this.data.building.id}))
  }

  onSubmit(building: IBuilding) {
    if (!this.buildingName?.valid) {
      return;
    }
    building = {
      ...building,
      buildingName: this.buildingName.value
    }
    this.store.dispatch(fromProjectActions.updateBuilding({ building: building }));
    this.isEditing = false;
  }

  onDelete(id: number) {
    this.store.dispatch(fromProjectActions.deleteBuilding({ id: id }));
    this.store.dispatch(fromModalDialogActions.closeModalDialog());
  }

  onEdit(buildingName: string) {
    this.isEditing = true;
    this.buildingName = new FormControl(buildingName, [Validators.required]);
  }

  onSubmitAdding(buildingBlockString: string, id: number) {
    this.store.dispatch(fromProjectActions.addNewBuildingBlock({
      buildingBlock: {
        buildingBlockName: buildingBlockString,
        isDone: false,
        buildingId: id
      } as IBuildingBlock
    }));
    this.isAdding = false;
  }

  deleteSubsection(id: number) {
    this.store.dispatch(fromProjectActions.deleteBuildingBlock({ id: id }));
  }

  editBuildingBlock(checked: boolean, buildingBlock: IBuildingBlock) {
    this.store.dispatch(fromProjectActions.updateBuildingBlock({
      buildingBlock: {
        ...buildingBlock,
        isDone: checked
      }
    }))
  }

  get servicesToShow() {
    return this.showAllServices ? this.services$ : this.singleService$;
  }

  get materialsToShow() {
    return this.showAllMaterials ? this.materials$ : this.singleMaterial$;
  }

  get documentsToShow() {
    return this.showAllDocuments ? this.documents$ : this.singleDocument$;
  }

  removeService(buildingId: number, serviceId: number) {
    this.store.dispatch(fromProjectActions.deleteBuildingService({ buildingId: buildingId, serviceId: serviceId }));
  }

  removeMaterial(id: number) {
    this.store.dispatch(fromProjectActions.deleteRequiredMaterial({ requiredMaterialId: id }));
  }

  removeDocument(id: number) {
    this.store.dispatch(fromProjectActions.deleteProjectDocument({ projectDocumentId: id }));
  }

  downloadDocument(document: { link: string, fileName: string }) {
    this.projectFileService.downloadFile(document.link, document.fileName);
  }

  closeAllOverlays() {
    this.isServicesOverlayOpen = false;
    this.isMaterialsOverlayOpen = false;
    this.isDocumentsOverlayOpen = false;
  }

  toggleServicesOverlay() {
    this.isServicesOverlayOpen = !this.isServicesOverlayOpen;
    this.isMaterialsOverlayOpen = false;
    this.isDocumentsOverlayOpen = false;
  }

  toggleMaterialsOverlay() {
    this.isServicesOverlayOpen = false;
    this.isMaterialsOverlayOpen = !this.isMaterialsOverlayOpen;
    this.isDocumentsOverlayOpen = false;
  }

  toggleDocumentsOverlay() {
    this.isServicesOverlayOpen = false;
    this.isMaterialsOverlayOpen = false;
    this.isDocumentsOverlayOpen = !this.isDocumentsOverlayOpen;
  }
}
