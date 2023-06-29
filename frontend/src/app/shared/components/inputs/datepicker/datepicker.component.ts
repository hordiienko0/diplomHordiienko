import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {FormControl} from "@angular/forms";
import {MatIconRegistry} from "@angular/material/icon";
import {DomSanitizer} from "@angular/platform-browser";
import {DatepickerHeaderComponent} from "./datepicker-header/datepicker-header.component";
import {MatCalendarCellClassFunction} from "@angular/material/datepicker";
import {FormControlState, NgrxValueConverter, NgrxValueConverters} from "ngrx-forms";
import {MomentDateAdapter} from "@angular/material-moment-adapter";
import * as moment from 'moment';


export const MY_FORMATS = {
  parse: {
    dateInput: 'LL',
  },
  display: {
    dateInput: 'DD.MM.YYYY',
    monthYearLabel: 'YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM'
  }
}

@Component({
  selector: 'app-datepicker',
  templateUrl: './datepicker.component.html',
  styleUrls: ['./datepicker.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class DatepickerComponent implements OnInit {

  datepickerHeader = DatepickerHeaderComponent;

  @Input() placeholder : string = ""
  @Input() control : FormControl | null = null;
  @Input() controlState: FormControlState<any> | null = null;

  dateClass: MatCalendarCellClassFunction<Date> = (cellDate, view) => {
    return 'example-custom-date-class';
  };

  dateValueConverter: NgrxValueConverter<Date | null, string | null> = {
    convertViewToStateValue(value) {
      if (value === null) {
        return null;
      }
      value = moment(value).toDate();
      // the value provided by the date picker is in local time but we want UTC so we recreate the date as UTC
      value = new Date(Date.UTC(value.getFullYear(), value.getMonth(), value.getDate()));
      return NgrxValueConverters.dateToISOString.convertViewToStateValue(value);
    },
    // tslint:disable-next-line: no-unbound-method
    convertStateToViewValue: NgrxValueConverters.dateToISOString.convertStateToViewValue,
  };

  constructor(private matIconRegistry: MatIconRegistry,
              private domSanitizer: DomSanitizer,) {
    this.matIconRegistry.addSvgIcon(
      'calendar',
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/calendar.svg")
    );
    this.matIconRegistry.addSvgIcon(
      'calendar-picked',
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/calendar-picked.svg")
    )
  }

  ngOnInit(): void {

  }

}
