import { Location } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store';
import { goBack } from 'src/app/store/actions/route.actions';

@Component({
  selector: 'app-auth-page-layout',
  templateUrl: './auth-page-layout.component.html',
  styleUrls: ['./auth-page-layout.component.scss']
})
export class AuthPageLayoutComponent {

  @Input() image = '/assets/images/login-buildings.png'

  constructor(private store: Store<AppState>) {
  }

  goBack() {
    this.store.dispatch(goBack())
  }

  get imageStyles() {
    return `background-image: url('${this.image}');`
  }
}
