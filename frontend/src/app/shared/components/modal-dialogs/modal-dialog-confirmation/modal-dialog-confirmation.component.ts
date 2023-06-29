import {Component, Input, OnInit} from '@angular/core';
import {AppState} from "../../../../store";
import {DomSanitizer} from "@angular/platform-browser";
import {Store} from "@ngrx/store";
import {MatIconRegistry} from "@angular/material/icon";
import {closeModalDialog} from "../../../../store/actions/modal-dialog.action";
import {ModalDialogService} from "../modal-dialog.service";

@Component({
  selector: 'app-modal-dialog-confirmation',
  templateUrl: './modal-dialog-confirmation.component.html',
  styleUrls: ['./modal-dialog-confirmation.component.scss']
})
export class ModalDialogConfirmationComponent implements OnInit {
  @Input() title: string = "";

  constructor(private service: ModalDialogService,
              private store: Store<AppState>,
              private domSanitizer: DomSanitizer,
              private matIconRegistry: MatIconRegistry) {
  }

  ngOnInit(): void {
    this.matIconRegistry.addSvgIcon(
      'cross',
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/Cross.svg")
    );
  }

  hideDialog() {
    this.store.dispatch(closeModalDialog())
  }
}
