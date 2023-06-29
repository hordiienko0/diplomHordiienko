import {ChangeDetectionStrategy, ChangeDetectorRef, Component, Inject, OnDestroy, OnInit} from '@angular/core';
import {Subject, takeUntil} from "rxjs";
import { MatCalendar, MatDatepickerIntl, yearsPerPage} from "@angular/material/datepicker";
import {DateAdapter, MAT_DATE_FORMATS, MatDateFormats} from "@angular/material/core";
import {MatIconRegistry} from "@angular/material/icon";
import {DomSanitizer} from "@angular/platform-browser";

@Component({
  selector: 'app-datepicker-header',
  templateUrl: './datepicker-header.component.html',
  styleUrls: ['./datepicker-header.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DatepickerHeaderComponent<D> implements OnDestroy {
  private _destroyed = new Subject<void>();

  constructor(public _calendar: MatCalendar<D>,
              private _dateAdapter: DateAdapter<D>,
              @Inject(MAT_DATE_FORMATS) private _dateFormats: MatDateFormats,
              cdr: ChangeDetectorRef,
              private _intl: MatDatepickerIntl,
              private matIconRegistry: MatIconRegistry,
              private domSanitizer: DomSanitizer,) {
    _calendar.stateChanges.pipe(takeUntil(this._destroyed)).subscribe(() => cdr.markForCheck());
    this.matIconRegistry.addSvgIcon(
      'caret-left',
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/caret-left.svg")
    );
    this.matIconRegistry.addSvgIcon(
      'caret-right',
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/caret-right.svg")
    );
    this.matIconRegistry.addSvgIcon(
      'caret-down',
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/caret-down.svg")
    );
    this.matIconRegistry.addSvgIcon(
      'caret-up',
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/caret-up.svg")
    );
  }

  ngOnDestroy(): void {
    this._destroyed.next();
    this._destroyed.complete();
  }

  get periodLabel() {
    if(this._calendar.currentView == 'month') {
      return this._dateAdapter
        .format(this._calendar.activeDate, this._dateFormats.display.monthYearA11yLabel);
    }

    if (this._calendar.currentView == 'year') {
      return this._dateAdapter.getYearName(this._calendar.activeDate);
    }

    const activeYear = this._dateAdapter.getYear(this._calendar.activeDate);
    const minYearOfPage =
      activeYear -
      this.getActiveOffset(
        this._dateAdapter,
        this._calendar.activeDate,
        this._calendar.minDate,
        this._calendar.maxDate,
      );
    const maxYearOfPage = minYearOfPage + yearsPerPage - 1;
    const minYearName = this._dateAdapter.getYearName(
      this._dateAdapter.createDate(minYearOfPage, 0, 1),
    );
    const maxYearName = this._dateAdapter.getYearName(
      this._dateAdapter.createDate(maxYearOfPage, 0, 1),
    );
    return this._intl.formatYearRange(minYearName, maxYearName);
  }

  get yearLabel() {
    return this._dateAdapter
      .format(this._calendar.activeDate, this._dateFormats.display.monthYearLabel);
  }

  previousClicked(mode: string) {
    this._calendar.activeDate =
      mode === 'month'
        ? this._dateAdapter.addCalendarMonths(this._calendar.activeDate, -1)
        : this._dateAdapter.addCalendarYears(
          this._calendar.activeDate, -yearsPerPage,
        );
  }

  nextClicked(mode: string) {
    this._calendar.activeDate =
      mode === 'month'
        ? this._dateAdapter.addCalendarMonths(this._calendar.activeDate, 1)
        : this._dateAdapter.addCalendarYears(
          this._calendar.activeDate, yearsPerPage,
        );
  }

  toggleCalendar() {
    this._calendar.currentView = this._calendar.currentView == 'month' ? 'multi-year' : 'month';
  }

  getActiveOffset<D>(
    dateAdapter: DateAdapter<D>,
    activeDate: D,
    minDate: D | null,
    maxDate: D | null,
  ): number {
    const activeYear = dateAdapter.getYear(activeDate);
    return this.euclideanModulo(activeYear - this.getStartingYear(dateAdapter, minDate, maxDate), yearsPerPage);
  }

  euclideanModulo(a: number, b: number): number {
    return ((a % b) + b) % b;
  }
  getStartingYear<D>(
    dateAdapter: DateAdapter<D>,
    minDate: D | null,
    maxDate: D | null,
  ): number {
    let startingYear = 0;
    if (maxDate) {
      const maxYear = dateAdapter.getYear(maxDate);
      startingYear = maxYear - yearsPerPage + 1;
    } else if (minDate) {
      startingYear = dateAdapter.getYear(minDate);
    }
    return startingYear;
  }
}
