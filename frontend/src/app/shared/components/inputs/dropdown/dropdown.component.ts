import { Component, Input, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormControl } from "@angular/forms";
import { Observable } from "rxjs";

export interface Option {
  value: any,
  viewValue: string
}


@Component({
  selector: 'app-dropdown',
  templateUrl: './dropdown.component.html',
  styleUrls: ['./dropdown.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class DropdownComponent {

  @Input() options: Observable<Option[]> | null = null;
  @Input() control: FormControl = new FormControl();
  @Input() placeholder: string = "";
  @Input() required: boolean = false;

}
