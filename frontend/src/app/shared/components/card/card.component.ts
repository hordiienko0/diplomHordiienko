import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ProjectStatus } from 'src/app/modules/project/resources/models/status';

export interface CardInformation {
  title: string;
  image: string;
  date: string;
  subtitle: string;
  id: number | null;
  status: ProjectStatus;
  statusBarLabel: string
  statusBarProgress: number;
  statusBarFull: number
}

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent {

  @Input() cardId: number = 0
  @Input() cardInformation: CardInformation = {
    title: '',
    image: '',
    date: '',
    subtitle: '',
    id: null,
    status: 0,
    statusBarLabel: '',
    statusBarProgress: 0,
    statusBarFull: 0
  }

  @Output() onCardClicked = new EventEmitter<number>();

  onClicked() {
    this.onCardClicked.emit(this.cardId);
  }

  get _dummyArray() {
    return Array.from({ length: this.cardInformation.statusBarFull }).map((_, i) => i);
  }
}
