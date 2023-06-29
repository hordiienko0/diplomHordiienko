import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Params } from "@angular/router";
import {Observable, of} from "rxjs";
import { ApiService } from 'src/app/core/resources/services/api.service';
import { IProjectPhotoId } from '../models/project-photo-id-response.model';
import { IProjectPhoto } from '../models/project-photo.model';
import { IResultId } from "src/app/modules/administration/resources/models/result-id.model";
import { PaginationModel } from "src/app/shared/models/pagination-model";
import { CreateProjectDTO } from "../models/createProjectDTO";
import { Order } from "../models/order";
import { IProjectDetailed } from "../models/project-details";
import { IProjectOverview } from "../models/project-overview";
import { IProjectUpdate } from "../models/project-update";
import { ProjectStatus } from "../models/status";
import { GetProjectTeamDto } from "../models/get-project-team-dto.model";
import { GetCompanyUsersDto } from "../models/get-company-users-dto";
import { NullLogger } from '@microsoft/signalr';
import { IProjectDocument } from '../models/project-documents/project-document.model';
import { IProjectDocumentId } from '../models/project-documents/project-document-id.model';
import { IProjectDocumentUpdate } from '../models/project-documents/project-document-update.model';
import {IService} from "../models/service";
@Injectable({
  providedIn: 'root',
})

export class ProjectService extends ApiService {
  apiPath: string = `/projects`;

  constructor(http: HttpClient) {
    super(http);
  }

  getProjectsWithParams(
    params: Params
  ): Observable<PaginationModel<IProjectOverview>> {
    return this.getWithOptions<PaginationModel<IProjectOverview>>(this.apiPath, {
      params: params
    });
  }

  getDetailedProject(
    id: number
  ): Observable<IProjectDetailed> {
    const path = `${this.apiPath}/${id}`
    return this.get<IProjectDetailed>(path);
  }

  putDetailedProject(
    data: IProjectUpdate
  ): Observable<IResultId> {

    const project = {
      ...data,
      startTime: new Date(data.startTime).toISOString(),
      endTime: new Date(data.endTime).toISOString()
    }

    return this.put<IResultId>(this.apiPath, project)
  }


  createProject(project: CreateProjectDTO) {
    return this.post(`${this.apiPath}`, project);
  }

  getProjectPhotos(projectId: number) {
    return this.get<IProjectPhoto[]>(`${this.apiPath}/${projectId}/photos`);
  }

  deleteProjectPhoto(projectId: number, projectPhotoId: number){
    return this.delete<IProjectPhotoId>(`${this.apiPath}/${projectId}/photos/${projectPhotoId}`)
  }

  changeStatus(projectId: number, newStatus: ProjectStatus) {
    return this.put(`${this.apiPath}/change-status`, { projectId, newStatus });
  }

  getProjectTeam(projectId: number) {
    return this.get<GetProjectTeamDto>(`${this.apiPath}/team?projectId=${projectId}`);
  }

  setProjectTeam(projectId: number, userIds: number[]) {
    return this.post(`${this.apiPath}/team`, { projectId, userIds });
  }

  getCompanyUsers(filter: string | null, sort: 'role' | 'firstName' | null): Observable<GetCompanyUsersDto> {
    return this.get<GetCompanyUsersDto>(`/users/?filter=${filter ?? ''}&sort=${sort ?? 'role'}`);
  }

  getProjectDocuments(id: number, buildingId: number | undefined, sort: 'created' | 'id', query?: string, order?: 1|0): Observable<IProjectDocument[]>{
    let request = `/projectDocuments/project/${id}?sort=${sort}`
    if(buildingId){
      request+=`&buildingId=${buildingId}`
    }
    if(query){
      request+=`&query=${query}`
    }
    if(order){
      request+=`&order=${order}`
    }
    return this.get<IProjectDocument[]>(request);
  }

  deleteProjectDocument(projectDocumentId: number): Observable<IProjectDocumentId>{
    return this.delete<IProjectDocumentId>(`/projectDocuments/${projectDocumentId}`);
  }

  updateProjectDocument(model: IProjectDocumentUpdate){
    return this.put<IProjectDocument>(`/projectDocuments`, model);
  }


}
