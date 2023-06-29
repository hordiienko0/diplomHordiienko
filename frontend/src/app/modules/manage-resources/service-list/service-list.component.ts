import { Component, OnInit } from '@angular/core';
import {MatIconRegistry} from "@angular/material/icon";
import {DomSanitizer} from "@angular/platform-browser";
import {Observable} from "rxjs";
import {IService} from "../resources/models/service";
import {select, Store} from "@ngrx/store";
import {AppState} from "../../../store";
import * as ServiceActions from '../state/manage-resources.actions';
import { selectServices, selectTypes} from "../state/manage-resources.selectors";
import {FormControl, FormGroup, Validators} from "@angular/forms";


@Component({
  selector: 'app-service-list',
  templateUrl: './service-list.component.html',
  styleUrls: ['./service-list.component.scss']
})


export class ServiceListComponent implements OnInit {

  services$: Observable<IService[]>;
  types$: Observable<string[]>;
  rowClicked: number = 0;
  displayedColumns: string[] = ['type', 'company', 'email', 'phone','website', 'action-first'];
  isAddClicked: boolean = false;
  isEdited: boolean = false;
  selectedService?:IService|null;
  selectedTypes?:string[]|null;
  sort: string = "company";
  sortedLine?: string;
  filter:string='';

  urlPattern:string = '^(?:http(s)?:\\/\\/)?[\\w.-]+(?:\\.[\\w\\.-]+)+[\\w\\-\\._~:\\?#[\\]@!\\$&\'\\(\\)\\*\\+,;=.]+$';

  types:FormControl<string[]|null> = new FormControl([]);
  company = new FormControl('', [Validators.required]);
  email = new FormControl('', [Validators.required, Validators.email]);
  phone = new FormControl('', [Validators.required, Validators.pattern('[- +()0-9]+')]);
  website = new FormControl('', [Validators.required, Validators.pattern(this.urlPattern)]);

  form = new FormGroup({
    types: this.types,
    company: this.company,
    email: this.email,
    phone: this.phone,
    website: this.website
  });


  constructor(private matIconRegistry: MatIconRegistry,
              private domSanitizer: DomSanitizer,
              private store: Store<AppState>) {
    this.matIconRegistry.addSvgIcon(
      'pencil',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        'assets/icons/pencil.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      'sort',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        'assets/icons/sort_column.svg'
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
    this.matIconRegistry.addSvgIcon(
      'copy',
      this.domSanitizer.bypassSecurityTrustResourceUrl('assets/icons/copy.svg')
    );

    this.store.dispatch(ServiceActions.getAllServicesWithParams({filter: "", sort: this.sort}))
    this.store.dispatch(ServiceActions.loadTypes());
    this.services$=this.store.pipe(select(selectServices));
    this.types$ = this.store.pipe(select(selectTypes));
  }


  ngOnInit(): void {

  }

  add(){
    if(this.isAddClicked==true){
      this.store.dispatch(ServiceActions.addClickFailure(
        {message:"You cannot add until another service is added."}))
    }
    else if(this.isEdited==true){
      this.store.dispatch(ServiceActions.addClickFailure(
        {message:"You cannot add until another service is edited."}))
    }
    else{
      this.isAddClicked=true;
      this.store.dispatch(ServiceActions.plusClicked());
    }
  }
  submitAdding(value: any){
    if(!this.form.valid || this.selectedTypes==null){
      this.store.dispatch(ServiceActions.serviceInvalid());
      return
    }
    if(this.isAddClicked){
      this.store.dispatch(ServiceActions.addSubmitted({service:{
          types:this.selectedTypes,
          company: this.form.value.company!,
          website: this.form.value.website!,
          phone: this.form.value.phone!,
          email:this.form.value.email! } as IService}));
      this.isAddClicked=false;
    }
    else{
      this.store.dispatch(ServiceActions.editSubmitted({service:{
          id: this.selectedService?.id,
          types:this.selectedTypes,
          company: this.form.value.company!,
          website: this.form.value.website!,
          phone: this.form.value.phone!,
          email:this.form.value.email! } as IService}));
      this.isEdited=false;
      this.selectedService=null;
    }
    this.selectedTypes=null;
    this.form.reset();
  }

  showData(data:IService){
    this.form.patchValue({
      types: data.types,
      company:data.company,
      email: data.email,
      website: data.website,
      phone: data.phone,
    });
    this.isEdited=true;
    this.selectedService = data;
    this.selectedTypes = data.types;
  }
  isEditRow(id:number):boolean {
    return this.selectedService!=null&&this.selectedService.id==id;
  }



  cancelEdit(){
    if(this.isEdited){
      this.selectedService = undefined;
      this.isEdited=false;
    }
    else{
      this.store.dispatch(ServiceActions.cancelAddClicked());
      this.isAddClicked=false;
    }
    this.form.reset();
    this.selectedTypes = null;
  }

  delete(id:number){
    console.log(id)
    this.store.dispatch(ServiceActions.deleteServiceSubmitted({id:id}))
  }

  recieveTypes($event: string[]) {
    this.selectedTypes = $event;
  }
  typeError() {
    if (this.types.hasError('required')) {
      return 'You should select types';
    }
    return '';
  }
  companyError(){
      if (this.company.hasError('required')) {
        return 'First name is required';
      }
      return '';
  }
  phoneError(){
    if (this.phone.hasError('required')) {
      return 'Phone number is required';
    }
    if (this.phone.hasError('pattern')) {
      return 'Phone number has wrong format';
    }
    return '';
  }

  websiteError(){
    if (this.website.hasError('required')) {
      return 'Website is required';
    }
    if (this.website.hasError('pattern')) {
      return 'Website has wrong format';
    }
    return '';
  }
  emailError(){
    if (this.website.hasError('required')) {
      return 'Email is required';
    }
    if (this.website.hasError('email')) {
      return 'Email has wrong format';
    }
    return '';
  }

  searchService($event: string) {
    this.filter = $event;
    this.store.dispatch(ServiceActions.getAllServicesWithParams({filter: $event, sort: this.sort}))
  }

  sortService(sort: string) {
    this.sort = sort;
    this.searchService(this.filter);
  }
}
