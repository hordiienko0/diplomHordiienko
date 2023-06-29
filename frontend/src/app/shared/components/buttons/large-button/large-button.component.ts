import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-large-button',
  templateUrl: './large-button.component.html',
  styleUrls: ['./large-button.component.scss']
})
export class LargeButtonComponent {

  @Input() disabled = false
  @Input() type: 'submit' | 'button' = 'button'

}
