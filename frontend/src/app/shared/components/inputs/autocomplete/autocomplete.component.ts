import {Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild, ViewEncapsulation} from '@angular/core';
import {FormControl} from "@angular/forms";
import {COMMA, ENTER} from "@angular/cdk/keycodes";
import {Observable, startWith} from "rxjs";
import {map} from "rxjs/operators";
import {MatChipInputEvent} from "@angular/material/chips";
import {MatAutocompleteSelectedEvent} from "@angular/material/autocomplete";
import {MatIconRegistry} from "@angular/material/icon";
import {DomSanitizer} from "@angular/platform-browser";

@Component({
  selector: 'app-autocomplete',
  templateUrl: './autocomplete.component.html',
  styleUrls: ['./autocomplete.component.scss'],
})
export class AutocompleteComponent implements OnInit {

  formControlInner : FormControl = new FormControl('');
  @Input() placeholder : string = '';
  @Input() options : string[] = [];
  @Input() label : string = '';
  @Input() inputOptions: string[]=[];

  @ViewChild('optionsInput') optionsInput? : ElementRef<HTMLInputElement>;

  _outputOptions : string[] = [];

  @Output() outputOptions = new EventEmitter<string[]>();

  filteredOptions : Observable<string[]>
  separatorKeysCodes: number[] = [ENTER, COMMA];

  constructor(private matIconRegistry: MatIconRegistry,
              private domSanitizer: DomSanitizer) {
    this.matIconRegistry.addSvgIcon(
      'delete',
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/delete.svg")
    );


    this.filteredOptions = this.formControlInner.valueChanges.pipe(
      startWith(null),
      map((option: string | null) => (option ? this._filter(option) : this.options.slice()))
    )
  }

  ngOnInit(): void {
    this._outputOptions=[...this.inputOptions];
  }

  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    if (value) {
      this._outputOptions.push(value);
    }

    event.chipInput!.clear();

    this.formControlInner.setValue(null);

    this.outputOptions.emit(this._outputOptions);
  }

  remove(fruit: string): void {
    const index = this._outputOptions.indexOf(fruit);

    if (index >= 0) {
      console.log(this._outputOptions)
      this._outputOptions.splice(index, 1);
      this.outputOptions.emit(this._outputOptions);
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this._outputOptions.push(event.option.viewValue);
    if (this.optionsInput){
      this.optionsInput.nativeElement.value = '';
    }
    this.formControlInner.setValue(null);
    this.outputOptions.emit(this._outputOptions);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.options.filter(option => option.toLowerCase().includes(filterValue));
  }

}
