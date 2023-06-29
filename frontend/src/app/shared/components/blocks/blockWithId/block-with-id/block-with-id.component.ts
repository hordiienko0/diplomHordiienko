import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-block-with-id',
  templateUrl: './block-with-id.component.html',
  styleUrls: ['./block-with-id.component.scss']
})
export class BlockWithIdComponent implements OnInit {

  @Input() id: number =0;
  @Input() name: string = "";

  constructor() { }

  ngOnInit() {
  }

}
