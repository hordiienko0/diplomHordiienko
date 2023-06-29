import { Component, OnInit } from '@angular/core';
import {Observable} from "rxjs";
import {select, Store} from "@ngrx/store";
import {AppState} from "../../../store";
import {selectProjectBuildings} from "../state/project.selectors";
import {addNewBuilding} from "../state/project.actions";

@Component({
  selector: 'app-building-section',
  templateUrl: './building-section.component.html',
  styleUrls: ['./building-section.component.scss']
})
export class BuildingSectionComponent implements OnInit {

  buildings$ = this.store.pipe(select(selectProjectBuildings));

  isAdding : boolean = false;

  constructor(private store : Store<AppState>) {
  }

  ngOnInit(): void {
  }

  onSubmitAdding(buildingName : string) {
    this.store.dispatch(addNewBuilding({buildingName: buildingName}));
    this.isAdding = false;
  }

}
