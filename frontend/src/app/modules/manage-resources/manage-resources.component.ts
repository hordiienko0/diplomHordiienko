import { Component, OnInit } from '@angular/core';
import {openMenu, revealMenu} from "../../store/actions/menu.actions";
import {Store} from "@ngrx/store";
import {AppState} from "../../store";


@Component({
  selector: 'app-resources-page',
  templateUrl: './manage-resources.component.html',
  styleUrls: ['./manage-resources.component.scss']
})
export class ManageResourcesComponent implements OnInit {

  constructor( private store: Store<AppState>) {
    this.store.dispatch(openMenu());
    this.store.dispatch(revealMenu());
  }

  ngOnInit(): void {
  }

}
