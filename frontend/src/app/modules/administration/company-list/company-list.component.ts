import { Component, OnInit } from '@angular/core';
import {AdministrationApiService} from "../resources/services/administration-api.service";
import {MatIconRegistry} from "@angular/material/icon";
import {DomSanitizer} from "@angular/platform-browser";
import {select, Store} from "@ngrx/store";
import {AppState} from "../../../store";
import * as fromAdministraionActions from '../state/administration.actions';
import {selectCompanies} from "../state/administration.selectors";
import {debounceTime, from, Observable, of, throttle, throttleTime} from "rxjs";
import {ICompanyOverview} from "../resources/models/company-overview.model";
import {FormControl} from "@angular/forms";
import {map, tap} from "rxjs/operators";
import {MatDialog} from "@angular/material/dialog";
import {CardInformation} from "../../../shared/components/card/card.component";
import {CreateCompany} from "../create-company/create-company.component";
import {openMenu, revealMenu, setMenuLinks} from "../../../store/actions/menu.actions";
import { getImageLink } from '../resources/utils';

@Component({
  selector: 'app-company-list',
  templateUrl: './company-list.component.html',
  styleUrls: ['./company-list.component.scss']
})
export class CompanyListComponent implements OnInit {

  searchInputDisabled : boolean = true;

  companies$? : Observable<ICompanyOverview[] | null>

  sort : string = "companyName";
  filter: string = "";

  filterInput = new FormControl("");

  constructor(private matIconRegistry: MatIconRegistry,
              private domSanitizer: DomSanitizer,
              private store: Store<AppState>,
              private dialog : MatDialog){
    this.matIconRegistry.addSvgIcon(
      'search',
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/search.svg")
    );
    this.matIconRegistry.addSvgIcon(
      'sort',
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/icons/sort.svg")
    );

    this.store.dispatch(openMenu());
    this.store.dispatch(revealMenu());
  }

  ngOnInit(): void {
    this.updateList();
    this.companies$ = this.store.pipe(select(selectCompanies));
  }



  toggleSort(value : string) {
    this.sort = value;
    this.updateList();
  }

  updateList(){
    this.store.dispatch(fromAdministraionActions.getAllCompaniesWithParams({filter: this.filter, sort: this.sort}));
  }

  openModal() {
    this.store.dispatch(fromAdministraionActions.updateNewCompanyId());
    this.dialog.open(CreateCompany, {
      maxWidth: '100vw',
      maxHeight: '100vh',
      height: '100%',
      width: '100%',
      panelClass: 'full-screen-modal',
    });
  }

  getCardInformation(company : ICompanyOverview) : CardInformation {
    return {
      title: company.companyName,
      subtitle: `${company.country}, ${company.city}, ${company.address}`,
      image: getImageLink(company.image),
      date: company.joinDate,
    } as CardInformation;
  }

  onSearchQuery($event: string) {
    this.filter = $event;
    this.store.dispatch(fromAdministraionActions.getAllCompaniesWithParams({filter: this.filter, sort: this.sort}))
  }
}
