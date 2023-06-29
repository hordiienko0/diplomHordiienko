import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Observable, of} from "rxjs";
import {IService} from "../../resources/models/service";
import {MatIconRegistry} from "@angular/material/icon";
import {DomSanitizer} from "@angular/platform-browser";
import {MatCheckboxChange} from "@angular/material/checkbox";
import * as ProjectActions from "../../state/project.actions"
import {AppState} from "../../../../store";
import {select, Store} from "@ngrx/store";
import * as fromProjectActions from "../../state/project.actions";
import {selectUncheckedBuildingServices} from "../../state/project.selectors";
import * as ServiceActions from "../../../manage-resources/state/manage-resources.actions";
import {loadUncheckedBuildingServices} from "../../state/project.actions";

@Component({
  selector: 'app-add-building-service',
  templateUrl: './add-building-service.component.html',
  styleUrls: ['./add-building-service.component.scss']
})
export class AddBuildingServiceComponent implements OnInit {

  @Input() buildingId: number = 0;
  @Input() projectId: number = 0;
  @Output() cancel = new EventEmitter<void>();
  @Output() submit = new EventEmitter<void>();
  selectedServices: IService[] = [];

  $services?:Observable<IService[]>;
  filter:string=''
  constructor(private iconRegistry: MatIconRegistry,
              private sanitizer: DomSanitizer,
              private store: Store<AppState>) {

  }

  ngOnInit(): void {
    this.store.dispatch(ProjectActions.loadUncheckedBuildingServices({filter: this.filter, buildingId: this.buildingId}));
    this.$services = this.store.pipe(select(selectUncheckedBuildingServices));
  }

  selectionChange($event: MatCheckboxChange, service: IService) {
    if($event.checked){
      this.selectService(service);
    }
    else{
      this.unselectService(service);
    }
  }
  selectService(service: IService){
    this.selectedServices.push(service);
  }
  unselectService(service: IService){
    let index = this.selectedServices.indexOf(service);
    this.selectedServices.splice(index,1);
  }
  submitServices(){
    this.store.dispatch(ProjectActions.submitCheckedServices({
      services: this.selectedServices,
      buildingId: this.buildingId
    }));
    this.submit.emit();
  }
  cancelServices(){
    this.selectedServices=[];
    this.cancel.emit();
  }
  searchService($event: string) {
    this.filter = $event;
    this.store.dispatch(ProjectActions.loadUncheckedBuildingServices({filter: $event, buildingId: this.buildingId}))
  }
}
