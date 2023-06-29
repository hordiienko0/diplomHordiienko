import {Component, Input, OnInit} from '@angular/core';
import {MatIconRegistry} from "@angular/material/icon";
import {DomSanitizer} from "@angular/platform-browser";
import {IBuilding} from "../resources/models/building.model";
import {MatDialog} from "@angular/material/dialog";
import {BuildingManagementDialogComponent} from "../building-management-dialog/building-management-dialog.component";
import {config} from "rxjs";
import {select, Store} from "@ngrx/store";
import {AppState} from "../../../store";
import {deleteBuildingBlock, revealBuildingCard, updateBuilding, updateBuildingBlock} from "../state/project.actions";
import {selectCurrentlyRevealedBuilding} from "../state/project.selectors";
import {IBuildingBlock} from "../resources/models/building-block.model";

@Component({
  selector: 'app-buildings-list-item',
  templateUrl: './buildings-list-item.component.html',
  styleUrls: ['./buildings-list-item.component.scss']
})
export class BuildingsListItemComponent implements OnInit {

  @Input() building?: IBuilding

  idRevealed$ = this.store.pipe(select(selectCurrentlyRevealedBuilding));
  isMouseOverContainer: boolean = false;
  isMouseOverSubsectionListItem: boolean = false;

  constructor(iconRegistry: MatIconRegistry,
              sanitizer: DomSanitizer,
              public dialog: MatDialog,
              private store: Store<AppState>) {
    iconRegistry.addSvgIcon(
      'caret-up',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/caret-up.svg')
    );
    iconRegistry.addSvgIcon(
      'caret-down',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/caret-down.svg')
    );
    iconRegistry.addSvgIcon(
      'cross',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/Cross.svg')
    );
    iconRegistry.addSvgIcon(
      'trash',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/trash.svg')
    );
    iconRegistry.addSvgIcon(
      'check',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/check.svg')
    );
    iconRegistry.addSvgIcon(
      'cross',
      sanitizer.bypassSecurityTrustResourceUrl('assets/icons/Cross.svg')
    );

  }

  ngOnInit(): void {
  }

  openDialog() {
    this.dialog.open(BuildingManagementDialogComponent, {
        data: {
          building: this.building,
        },
      }
    );
  }

  deleteSubsection(id: number) {
    this.store.dispatch(deleteBuildingBlock({id: id}));
  }

  toggleRevealed(id: number) {
    this.store.dispatch(revealBuildingCard({id: id}))
  }

  editBuildingBlock(checked: boolean, buildingBlock:IBuildingBlock) {
    this.store.dispatch(updateBuildingBlock({
      buildingBlock: {
        ...buildingBlock,
        isDone: checked
      }
    }))
  }
}
