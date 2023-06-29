import {Injectable} from '@angular/core';
import {ApiService} from "../../../../core/resources/services/api.service";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ICompanyProfile} from "../models/company-profile";
import {Params} from "@angular/router";
import {PaginationModel} from "../../../../shared/models/pagination-model";
import {IProjectOverview} from "../models/project-overview";
import {ICompanyUpdate} from "../models/company-update";
import { ICompanyLogo } from '../models/company-logo';
import { deleteCompanyLogo } from '../../state/company.actions';
import { ICompanyLogoId } from '../models/company-logo-id';

@Injectable({
  providedIn: 'root'
})
export class CompanyApiService extends ApiService {

  apiUrl = "/companies";

  constructor(http: HttpClient) {
    super(http);
  }

  getCompanyProfile(userId: number): Observable<ICompanyProfile> {
    return this.get<ICompanyProfile>(`${this.apiUrl}/get-by-user-id/${userId}`);
  }

  getProjectsByCompanyId(
    id: number
  ): Observable<IProjectOverview[]> {
    return this.get<IProjectOverview[]>(`/projects/company/${id}`);
  }

  putCompanyProfile(company : ICompanyUpdate) : Observable<ICompanyProfile> {
    return this.put<ICompanyProfile>(`${this.apiUrl}/company-profile/${company.id}`, company);
  }

  getLogoByCompanyId(id: number): Observable<ICompanyLogo>{
    return this.get<ICompanyLogo>(`${this.apiUrl}/${id}/logo`);
  }

  deleteCompanyLogo(id: number): Observable<ICompanyLogoId>{
    return this.delete<ICompanyLogoId>(`${this.apiUrl}/${id}/logo`);
  }
}
