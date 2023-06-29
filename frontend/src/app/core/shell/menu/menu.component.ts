import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import {MatIconRegistry} from "@angular/material/icon";
import {DomSanitizer} from "@angular/platform-browser";
import {select, Store} from "@ngrx/store";
import {AppState} from "../../../store";
import {Observable} from "rxjs";
import {Router} from "@angular/router";
import {IMenuLink} from "./resources/IMenuLink";
import * as fromMenuSelectors from "../../../store/selectors/menu.selectors";
import * as fromMenuActions from "../../../store/actions/menu.actions";
import {logout} from "../../../store/actions/auth.actions";
import {goBack} from "../../../store/actions/route.actions";
import { NotificationService } from '../../../shared/services/notification.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit, AfterViewInit, OnDestroy {

  opened$? : Observable<boolean>
  revealed$? : Observable<boolean>

  selectedRoute? : string

  routes? : Observable<IMenuLink[]>


  constructor(private matIconRegistry: MatIconRegistry,
              private domSanitizer: DomSanitizer,
              private store : Store<AppState>,
              private router : Router,
              private notifService: NotificationService) {
    this.matIconRegistry.addSvgIcon(
      'caret-left-grey',
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/caret-left-grey.svg")
    );
  }

  hide() {
    this.store.dispatch(fromMenuActions.hideMenu());
  }

  reveal() {
    this.store.dispatch(fromMenuActions.revealMenu());
  }

  ngOnInit(): void {
    this.selectedRoute = this.router.url;
    this.routes = this.store.pipe(select(fromMenuSelectors.selectMenuLinks));
    this.opened$ = this.store.pipe(select(fromMenuSelectors.selectIfMenuIsOpened));
    this.revealed$ = this.store.pipe(select(fromMenuSelectors.selectIfMenuIsRevealed));
  }

  ngAfterViewInit() {
    this.notifService.UpdateConnection();
  }

  ngOnDestroy() {
    this.notifService.stopService();
  }

  onSelect(route: string){
    this.selectedRoute = route;
  }

  logout() {
    this.store.dispatch(logout());
  }
}
