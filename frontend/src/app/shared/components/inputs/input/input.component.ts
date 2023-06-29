import { FormControl } from '@angular/forms';
import { FormControlState } from 'ngrx-forms';
import { Component, Input, OnInit } from '@angular/core';
import { TextInputType } from 'src/app/shared/types/input-types';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss']
})
export class InputComponent implements OnInit {

  @Input() control: FormControl | null = null;
  @Input() label: string = ""
  @Input() placeholder: string = ""
  @Input() errorMessage: string = ""
  @Input() type: TextInputType = "text"
  @Input() controlState: FormControlState<any> | null = null;

  get title() {
    return this.control?.invalid && (this.control.touched || this.controlState?.isTouched) ? this.errorMessage : this.label;
  }

  ngOnInit(): void {
    // in some cases we don't need to specify the form control (for example when using (input)="" instead)
    // but without it there will be an error so we set default value here
    if (!this.control && !this.controlState) {
      this.control = new FormControl();
    }
  }

}
