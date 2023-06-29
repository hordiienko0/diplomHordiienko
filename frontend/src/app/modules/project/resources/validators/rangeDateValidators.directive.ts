import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export const RangeDateValidators: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const startDateString = control.get('startDate');
  const endDateString = control.get('endDate');
  const startDate = new Date(startDateString?.value)
  const endateDate = new Date(endDateString?.value)

  return startDate>endateDate ? { identityRevealed: true } : null;
}