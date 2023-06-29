import {Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {FormControl} from "@angular/forms";
import {Observable, startWith} from "rxjs";
import {map} from "rxjs/operators";



@Component({
  selector: 'app-default-autocomplete',
  templateUrl: './default-autocomplete.component.html',
  styleUrls: ['./default-autocomplete.component.scss']
})
export class DefaultAutocompleteComponent implements OnInit {
@Input() control = new FormControl<string>('');
  @Input() options: string[] = [];
  @Input() label: string = '';
  filteredOptions!: Observable<string[]>;

  constructor() {
  }

  ngOnInit() {
    this.filteredOptions = this.control.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value || '')),
    );
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.options.filter(option => option.toLowerCase().includes(filterValue));
  }
}
