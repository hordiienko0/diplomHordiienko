import { Component, Input } from '@angular/core';
import { Router } from "@angular/router";
import { AppState } from "../../../../store";
import { Store } from "@ngrx/store";
import * as fromRouteActions from "../../../../store/actions/route.actions";

@Component({
  selector: 'app-underlined-button',
  templateUrl: './underlined-button.component.html',
  styleUrls: ['./underlined-button.component.scss']
})

export class UnderlinedButtonComponent {

  @Input() routerLink: string = ''
  @Input() click = () => this.store.dispatch(fromRouteActions.navigate({ commands: [this.routerLink] }));

  constructor(private router: Router, private store: Store<AppState>) {
  }

}
