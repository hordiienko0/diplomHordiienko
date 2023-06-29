import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { TextInputType } from 'src/app/shared/types/input-types';

@Component({
  selector: 'app-mini-button',
  templateUrl: './mini-button.component.html',
  styleUrls: ['./mini-button.component.scss'],
})
export class MiniButtonComponent implements OnInit {

  @Input() disabled = false;
  @Input() type?: string = "button"

  constructor() { }

  ngOnInit(): void {
  }

}
