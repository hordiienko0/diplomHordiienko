import { Injectable } from '@angular/core';
import {ApiService} from "../../../../core/resources/services/api.service";
import {HttpClient} from "@angular/common/http";
import {Observable, of} from "rxjs";
import {IBuilding} from "../models/building.model";
import {IBuildingBlock} from "../models/building-block.model";
import {IService} from "../models/service";

@Injectable({
  providedIn: 'root'
})
export class BuildingApiService extends ApiService {

  apiPath = '/buildings'

  constructor(http: HttpClient) {
    super(http);
  }

  getBuildingsByProjectId(projectId : number) : Observable<IBuilding[]> {
    return this.get<IBuilding[]>(`${this.apiPath}/project/${projectId}`);
  }

  addBuilding(building : IBuilding) : Observable<void> {
    return this.post<void>(`${this.apiPath}`, {
      buildingName: building.buildingName,
      projectId: building.projectId
    });
  }

  updateBuilding(building : IBuilding) : Observable<void> {
    return this.put<void>(`${this.apiPath}/${building.id}`,{
      id: building.id,
      buildingName: building.buildingName
    });
  }

  deleteBuilding(id : number) : Observable<void> {
    return this.delete<void>(`${this.apiPath}/${id}`);
  }

  addBuildingBlock(buildingBlock : IBuildingBlock) : Observable<void> {
    return this.post<void>(`/building-blocks`, {
      buildingBlockName : buildingBlock.buildingBlockName,
      buildingId : buildingBlock.buildingId
    });
  }

  deleteBuildingBlock(id : number) : Observable<void> {
    return this.delete<void>(`/building-blocks/${id}`);
  }

  updateBuildingBlock(buildingBlock : IBuildingBlock) : Observable<void> {
    return this.put<void>(`/building-blocks/${buildingBlock.id}`,{
      id: buildingBlock.id,
      isDone: buildingBlock.isDone
    });
  }


}
