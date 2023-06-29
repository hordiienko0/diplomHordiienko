import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormControl, Validators} from "@angular/forms";

@Component({
  selector: 'app-building-adding-list-item',
  templateUrl: './building-adding-list-item.component.html',
  styleUrls: ['./building-adding-list-item.component.scss']
})
export class BuildingAddingListItemComponent implements OnInit {

  @Input() placeholder : string = "";

  @Output() cancelClicked = new EventEmitter();
  @Output() submitClicked = new EventEmitter<string>();

  buildingName = new FormControl('', [Validators.required]);

  constructor() { }

  ngOnInit(): void {
  }

  onCancel() : void {
    this.cancelClicked.emit();
  }

  onSubmit() : void {
    if (this.buildingName.value == null) {
      return;
    }
    this.submitClicked.emit(this.buildingName.value);
    this.buildingName.reset();
  }

}
