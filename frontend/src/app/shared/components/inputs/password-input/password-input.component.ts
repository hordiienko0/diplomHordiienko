import { Component, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { TextInputType } from 'src/app/shared/types/input-types';
import { InputComponent } from '../input/input.component';



@Component({
  selector: 'app-password-input',
  templateUrl: './password-input.component.html',
  styleUrls: ['./password-input.component.scss']
})
export class PasswordInputComponent extends InputComponent {
  hide: boolean = true
  constructor(private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer){
    super()
    this.type = "password"
    this.matIconRegistry.addSvgIcon(
      "Eye", 
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/Eye.svg")
    )
    this.matIconRegistry.addSvgIcon(
      "EyeSlash", 
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/EyeSlash.svg")
    )
  }

  get changedType(): TextInputType {
    return this.hide ? "password" : "text"
  }
}
