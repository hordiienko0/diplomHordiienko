import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class NumberGenarateSevice {
  private readonly countNumberInId = 8;
  generateId(): number {
    return Math.floor(Math.random() * Math.pow(10, this.countNumberInId))
  }
}