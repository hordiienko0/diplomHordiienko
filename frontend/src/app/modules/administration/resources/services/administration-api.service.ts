import { Injectable } from '@angular/core';
import { ApiService } from '../../../../core/resources/services/api.service';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { ICompanyOverview } from '../models/company-overview.model';
import { IMember } from '../models/member.model';
import { INewCompanyDto } from '../DTOmodels/new-company-dto.model';
import { ICompanyDetailed } from '../models/company-detailed.model';
import { ICompanyUpdate } from '../models/company-update.model';
import { IRole } from "../models/role.model";
import { IResultId } from '../models/result-id.model';
import { UserDetailsDto } from '../models/userDetailsDto';
import { Params } from '../models/params.model';
import { PaginationModel } from 'src/app/shared/models/pagination-model';
import { ICompanyProject } from '../models/company-project.model';

@Injectable({
  providedIn: AdministrationApiService,
})
export class AdministrationApiService extends ApiService {
  ApiPath: string = '';
  UserApi = "/users"
  constructor(http: HttpClient) {
    super(http);
  }

  getUserDetails(userId: number): Observable<UserDetailsDto> {
    return this.get<UserDetailsDto>(`${this.UserApi}/details/${userId}`);
  }

  postMember(data: IMember): Observable<number> {
    return this.post<number>(`/users`, data);
  }

  createCompany(data: INewCompanyDto): Observable<any> {
    return this.post('/companies/create', data);
  }

  getAllCompaniesWithParameters(filter: string, sort: string): Observable<ICompanyOverview[]> {
    let query = `/companies?sort=${sort}`;
    if (filter != "") {
      query += `&filter=${filter}`;
    }
    return this.get<ICompanyOverview[]>(query);
  }

  getDetailedCompany(id: number): Observable<ICompanyDetailed> {
    return this.get<ICompanyDetailed>(
      `/companies/${id}`
    );
  }

  getCompanyProjects(id: number, params: Params) : Observable<PaginationModel<ICompanyProject>>{
    return this.getWithOptions<PaginationModel<ICompanyProject>>(`/companies/${id}/projects`, {params: params})
  }

  postCompanyImage(id: Number, image: File): Observable<string> {
    return this.post<string>(`${this.ApiPath}\\postCompanyImage\\${id}`, image);
  }

  getAllRoles(): Observable<IRole[]> {
    return this.get<IRole[]>('/users/roles');
  }

  putDetailedCompany(company: ICompanyUpdate): Observable<IResultId> {
    return this.put<IResultId>('/companies/', company);
  }

  getMembersByCompanyId(companyId: number): Observable<IMember[]> {
    return this.get<IMember[]>(`/users/byCompanyId/${companyId}`);
  }

  getNewGeneratedCompanyId(): Observable<number> {
    return this.get<number>(`/companies/newCompanyId`);
  }
}
