import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormControl} from "@angular/forms";
import {debounceTime, Observable} from "rxjs";
import {map, tap} from "rxjs/operators";
import {MatIconRegistry} from "@angular/material/icon";
import {DomSanitizer} from "@angular/platform-browser";

@Component({
  selector: 'app-filter-input',
  templateUrl: './filter-input.component.html',
  styleUrls: ['./filter-input.component.scss']
})
export class FilterInputComponent implements OnInit {

  @Input() label : string = '';
  @Input() placeholder : string = '';

  formControl = new FormControl('');
  filter$? : Observable<string>

  @Output() onSearchQuery = new EventEmitter<string>();

  constructor(private matIconRegistry: MatIconRegistry,
              private domSanitizer: DomSanitizer) {
    this.matIconRegistry.addSvgIcon(
      'search',
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/search.svg")
    );
    this.matIconRegistry.addSvgIcon(
      'cross',
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/Cross.svg")
    );
  }

  ngOnInit(): void {
    this.filter$ = this.formControl.valueChanges.pipe(
      debounceTime(500),
      map(v => v as string),
      tap(filter => this.onSearchQuery.emit(filter))
    );
  }

  barVisible : boolean = false;

  toggleInputBar() {
    this.barVisible = !this.barVisible;
  }


  onCrossClicked() {
    this.formControl.reset();
  }
}
