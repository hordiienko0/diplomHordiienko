import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ProjectFileService } from "../../resources/services/project-file.services";
import { Store } from "@ngrx/store";
import { AppState } from "../../../../store";
import * as fromProjectActions from "../../state/project.actions"
import { FormControl } from "@angular/forms";
import { Observable } from 'rxjs';
import { AvailableMaterial } from '../../resources/models/project-material/available-material.model';
import { MatCheckbox, MatCheckboxChange } from "@angular/material/checkbox";
import { ResourceApiService } from '../../resources/services/resource-api.service';
import { RequiredMaterial } from '../../resources/models/project-material/required-material.model';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { AlertService } from 'src/app/modules/alert/resources/services/alert.service';
import { AlertType } from '../../../alert/resources/types/alert-type';
@Component({
  selector: 'add-building-material-overlay',
  templateUrl: './add-building-material-overlay.component.html',
  styleUrls: ['./add-building-material-overlay.component.scss']
})
export class AddBuildingMaterialOverlay {

  @Input() buildingId: number = 0;
  @Input() projectId: number = 0;
  @Output() cancel = new EventEmitter<void>();
  @Output() submit = new EventEmitter<void>();
  filter : string;
  availableMaterials$ : Observable<AvailableMaterial[]>;
  chosenMaterials: RequiredMaterial[];
  
  constructor(
    private filesService: ProjectFileService,
    private store: Store<AppState>,
    private resourceApi: ResourceApiService,
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer,
    private alertService: AlertService
  ) {
    iconRegistry.addSvgIcon(
      'delete',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/delete.svg')
    );
    this.filter = "";
    this.chosenMaterials = [];
    this.availableMaterials$ = this.resourceApi.getAvailableMaterials(this.filter);
  }

  selectionChange(event: MatCheckboxChange, material: AvailableMaterial){
      if(event.checked){
          this.chosenMaterials.push({
              id: material.id,
              amount: 1,
              materialTypeName: material.materialTypeName,
              measurementName: material.measurementName,
              projectId: this.projectId,
              buildingId: this.buildingId,
              price: material.price,
              maxamount: material.amount
          });
      } else {
          this.chosenMaterials = this.chosenMaterials.filter(el => el.id != material.id);
      }
  }

  isSelected(id: number): boolean{
    if(this.chosenMaterials.find(el => el.id == id) != undefined) return true;
    return false;
  }

  cancelSelected(){
    this.cancel.emit();
    this.chosenMaterials = [];
  }

  submitSelected(){
    for(let i = 0; i < this.chosenMaterials.length;i++){
      if(this.chosenMaterials[i].amount > this.chosenMaterials[i].maxamount){
        var message: string = "Not enought material: "+ this.chosenMaterials[i].materialTypeName
        + ". You asked for: " + this.chosenMaterials[i].amount + " " + this.chosenMaterials[i].measurementName + ".Available: " +
        this.chosenMaterials[i].maxamount + " " + this.chosenMaterials[i].measurementName;
          this.alertService.showAlert(message, "OK", "error");
          return;
      }
    } 
    this.store.dispatch(fromProjectActions.saveRequiredMaterials( {materials: this.chosenMaterials} ));
    this.cancel.emit();
    this.chosenMaterials = [];
  }
  
  getTotal(id: number): number{
    var mater = this.chosenMaterials.find(el => el.id == id);
    if(mater != undefined){
      return mater.amount * mater.price
    }
    return 0;
  }

  changeAmount(event: any, id: number){
    const number = event.target.value;
    var mater = this.chosenMaterials.find(el => el.id == id);
    if(mater != undefined){
      mater.amount = number;
    }

  }

  startValue(id:number):number {
    var mater = this.chosenMaterials.find(el => el.id == id);
    if(mater != undefined){
       return mater.amount;
    }
    return 1;
  }

  search(event: any) {
    const filter = event.target.value;
    console.log(filter);
    this.availableMaterials$ = this.resourceApi.getAvailableMaterials(filter);
  }

  unselectUser(material: RequiredMaterial){
    this.chosenMaterials = this.chosenMaterials.filter(el => el.id != material.id);
  }
}
