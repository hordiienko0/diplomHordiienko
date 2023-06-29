import { Component, ElementRef, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss'],
})
export class ButtonComponent implements OnInit {

  @Input() disabled = false;
  @Input() type: 'submit' | 'button' = 'button'

  constructor() { }

  ngOnInit(): void {

  }

}
