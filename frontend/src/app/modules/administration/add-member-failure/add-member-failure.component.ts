import {Component, Inject, OnInit} from '@angular/core';
import {select, Store} from "@ngrx/store";
import {MatIconRegistry} from "@angular/material/icon";
import {DomSanitizer} from "@angular/platform-browser";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {mergeMap, Observable, of} from "rxjs";
import {selectErrorLines, selectRoles} from "../state/administration.selectors";
import {map} from "rxjs/operators";

@Component({
  selector: 'app-add-member-failure',
  templateUrl: './add-member-failure.component.html',
  styleUrls: ['./add-member-failure.component.scss']
})
export class AddMemberFailureComponent implements OnInit {

  failures$?: Observable<string[]|undefined>
  constructor(private store:Store,
              private matIconRegistry: MatIconRegistry,
              private domSanitizer: DomSanitizer,
              private dialogRef: MatDialogRef<AddMemberFailureComponent>) {
    this.matIconRegistry.addSvgIcon(
      'cross',
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/Cross.svg")
    );

    this.failures$=this.store.pipe(
        select(selectErrorLines));


  }

  ngOnInit(): void {

  }


}
