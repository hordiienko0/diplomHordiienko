import {Component, OnInit} from '@angular/core';
import {AppState} from "../../../store";
import {select, Store} from "@ngrx/store";
import {MatIconRegistry} from "@angular/material/icon";
import {DomSanitizer} from "@angular/platform-browser";
import * as MaterialAction from "../state/manage-resources.actions"
import {Observable} from "rxjs";
import {IMaterial} from "../resources/models/material-dto";
import {
  selectMaterials,
  selectMaterialTypes,
  selectMeasurement
} from "../state/manage-resources.selectors";
import {Option} from "../../../shared/components/inputs/dropdown/dropdown.component";
import {map} from "rxjs/operators";
import {
  addMaterialSubmitted,
  addSubmitted,
  cancelAddClicked,
  deleteMaterialSubmitted, editMaterialSubmitted, editSubmitted, getMaterials,
  plusClicked,
  plusMaterialClicked
} from "../state/manage-resources.actions";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {IMaterialType} from "../resources/models/material-type-dto";

@Component({
  selector: 'app-material',
  templateUrl: './material.component.html',
  styleUrls: ['./material.component.scss']
})
export class MaterialComponent implements OnInit {
  materials$!: Observable<IMaterial[]>;
  type$: Observable<IMaterialType[]>;
  measurement$: Observable<Option[]> = this.store.pipe(
    select(selectMeasurement),
    map(measurement => measurement == null
      ? []
      : measurement.map<Option>(measurement => ({value: measurement.name, viewValue: measurement.name})))
  );

  displayedColumns: string[] = ['materialType', 'companyName', 'companyAddress', 'measurement', 'amount', 'price', 'totalAmount', 'date', 'action-first'];

  selectedMaterial?: IMaterial | null;
  selectedTypes?: string | null;
  measurementType?: string | null
  isAddClicked: boolean = false;
  isEdited: boolean = false;

  materialType = new FormControl('', [Validators.required]);
  companyName = new FormControl('', [Validators.required]);
  companyAddress = new FormControl('', [Validators.required]);
  price = new FormControl(0, [Validators.required]);
  amount = new FormControl(0, [Validators.required]);
  measurement = new FormControl('', [Validators.required]);
  date = new FormControl('', [Validators.required]);
  totalAmount = new FormControl(0, [Validators.required]);

  form = new FormGroup({
    materialType: this.materialType,
    companyName: this.companyName,
    companyAddress: this.companyAddress,
    price: this.price,
    amount: this.amount,
    measurement: this.measurement,
    date: this.date,
    totalAmount: this.totalAmount,
  });

  constructor(private store: Store<AppState>,
              private matIconRegistry: MatIconRegistry,
              private domSanitizer: DomSanitizer) {
    this.matIconRegistry.addSvgIcon(
      'pencil',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        'assets/icons/pencil.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      'trash',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        'assets/icons/trash.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      'plus',
      this.domSanitizer.bypassSecurityTrustResourceUrl('assets/icons/plus.svg')
    );
    this.matIconRegistry.addSvgIcon(
      'approve',
      this.domSanitizer.bypassSecurityTrustResourceUrl('assets/icons/Vector.svg')
    );
    this.matIconRegistry.addSvgIcon(
      'cancel',
      this.domSanitizer.bypassSecurityTrustResourceUrl('assets/icons/Cross.svg')
    );
    this.store.dispatch(MaterialAction.getMaterials());
    this.store.dispatch(MaterialAction.loadMaterialTypes())
    this.store.dispatch(MaterialAction.loadMeasurement())
    this.materials$ = this.store.pipe(select(selectMaterials))
    this.type$ = this.store.pipe(select(selectMaterialTypes))
  }

  ngOnInit(): void {

  }

  add() {
    this.isAddClicked = true;
    this.store.dispatch(plusMaterialClicked());
  }

  isEditRow(id: number): boolean {
    return this.selectedMaterial != null && this.selectedMaterial.id == id;
  }

  showData(data: IMaterial) {
    this.form.patchValue({
      materialType: data.materialType,
      companyName: data.companyName,
      companyAddress: data.companyAddress,
      price: data.price,
      amount: data.amount,
      measurement: data.measurement,
    });
    this.isEdited = true;
    this.selectedMaterial = data;
    this.selectedTypes = data.materialType;
    console.log(this.selectedTypes);
  }

  submitAdding(value: any) {
    // if(!this.form.valid){
    //   return
    // }
    if (this.isAddClicked) {
      this.store.dispatch(addMaterialSubmitted({
        material: {
          materialType: this.form.value.materialType,
          companyName: this.form.value.companyName!,
          companyAddress: this.form.value.companyAddress!,
          measurement: this.form.value.measurement!,
          price: this.form.value.price!,
          amount: this.form.value.amount!,

        } as IMaterial
      }));

      this.isAddClicked = false;
    } else {
      this.store.dispatch(editMaterialSubmitted({
        material: {
          id: this.selectedMaterial?.id,
          materialType: this.form.value.materialType,
          companyName: this.form.value.companyName!,
          companyAddress: this.form.value.companyAddress!,
          price: this.form.value.price,
          amount: this.form.value.amount,
          measurement: this.form.value.measurement,
        } as IMaterial
      }));
      this.isEdited = false;
      this.selectedMaterial = null;
    }
    this.selectedTypes = null;
    this.form.reset();
  }

  delete(id: number) {
    console.log(id)
    this.store.dispatch(deleteMaterialSubmitted({id: id}))
  }

  cancelEdit() {
    if (this.isEdited) {
      this.selectedMaterial = undefined;
      this.selectedTypes = undefined;
      this.measurementType = undefined;
      this.isEdited = false;
    } else {
      this.store.dispatch(cancelAddClicked());
      this.isAddClicked = false;
    }
    this.form.reset();
    this.selectedTypes = null;
  }

  mapToString(): Observable<string[]> {
    return this.type$.pipe(map(materialTypes => materialTypes.map(materialType => materialType.name)))
  }

  companyError() {
    if (this.companyName.hasError('required')) {
      return 'Name of company is required';
    }
    return '';
  }

  companyAddressError() {
    if (this.companyName.hasError('required')) {
      return 'Address of company is required';
    }
    return '';
  }

  priceError() {
    if (this.companyName.hasError('required')) {
      return 'Price is required';
    }
    return '';
  }

  amountError() {
    if (this.companyName.hasError('required')) {
      return 'Amount is required';
    }
    return '';
  }

}
