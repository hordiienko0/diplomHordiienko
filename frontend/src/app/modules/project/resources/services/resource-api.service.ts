import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, of} from "rxjs";
import {IBuilding} from "../models/building.model";
import { ApiService } from "../../../../core/resources/services/api.service";
import { GetRequiredMaterialsDtoModel } from "../models/get-required-materials-dto.model";
import { AvailableMaterial } from '../models/project-material/available-material.model';
import { RequiredMaterial } from '../models/project-material/required-material.model';
import {IService} from "../models/service";
import { UsedByProjectMaterial } from '../models/project-material/project-used-material.model'

@Injectable({
  providedIn: 'root'
})
export class ResourceApiService extends ApiService {

  apiPath = '/resources'

  constructor(http: HttpClient) {
    super(http);
  }

  getAvailableMaterials(filter: string | null): Observable<AvailableMaterial[]> {
    return this.get<AvailableMaterial[]>(`${this.apiPath}/available-materials?filter=${filter ?? ''}`);
  }

  saveRequiredMaterials(materials: RequiredMaterial[]): Observable<void> {
    return this.post<void>(`${this.apiPath}/save-required`, materials);
  }

  createReport(projectId: number) : Observable<void>{
    return this.post<void>(`${this.apiPath}/create-report/${projectId}`, null)
  }

  getAllUsedMaterials(projectid: number, filter: string | null, sort: string | null): Observable<UsedByProjectMaterial[]>{
    return this.get<UsedByProjectMaterial[]>(`${this.apiPath}/materials-of-project?&projectid=${projectid}&filter=${filter ?? ''}&sort=${sort ?? 'role'}`);
  }

  getRequiredMaterials(buildingId: number): Observable<GetRequiredMaterialsDtoModel> {
    return this.get<GetRequiredMaterialsDtoModel>(`${this.apiPath}/required-materials?buildingId=${buildingId}`);
  }

  deleteRequiredMaterial(requiredMaterialId: number): Observable<void> {
    return this.delete(`${this.apiPath}/required-materials/${requiredMaterialId}`);
  }
  postBuildingServices(services: IService[], buildingId: number): Observable<IService[]>{
    return this.post<IService[]>(`${this.apiPath}/add/services`, {
      buildingId: buildingId,
      serviceIds: services.map(x=>x.id)
    });
  }
  getUnselectedBuildingServices(buildingId: number, filter: string): Observable<IService[]>{
    let query = `${this.apiPath}/available-services/${buildingId}`;
    if (filter != "") {
      query += `?filter=${filter}`;
    }
    return this.get<IService[]>(query);
  }
  deleteSelectedService(buildingId:number, serviceId:number){
    return this.delete(`${this.apiPath}/building/${buildingId}/delete-service/${serviceId}`)
  }
  getSelectedBuildingServices(buildingId: number): Observable<IService[]>{
    return this.get<IService[]>(`${this.apiPath}/selected-services/${buildingId}`);
  }
}
