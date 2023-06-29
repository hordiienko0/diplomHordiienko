import {Injectable} from "@angular/core";
import {ApiService} from "../../../../core/resources/services/api.service";
import {HttpClient} from "@angular/common/http";
import {IService} from "../models/service";
import {Observable, of, tap} from "rxjs";
import {Params} from "@angular/router";
import {PaginationModel} from "../../../../shared/models/pagination-model";
import {IMaterial} from "../models/material-dto";
import {IMaterialType} from "../models/material-type-dto";
import {IMeasurement} from "../models/measurement-dto";

@Injectable({
  providedIn: 'root'
})
export class ManageResourcesApiService extends ApiService {

  apiUrl = "/resources";

  constructor(http: HttpClient) {
    super(http);
  }

  addService(service: IService): Observable<IService> {

    return this.post<IService>(`${this.apiUrl}/service/add`, service);
  }

  editService(service: IService): Observable<IService> {
    return this.put<IService>(`${this.apiUrl}/service/edit`, service);
  }

  loadServices(userId: number): Observable<IService[]> {
    return this.get<IService[]>(`${this.apiUrl}/services/by-user-id/${userId}`);
  }

  deleteService(id: number): Observable<number> {
    return this.delete<number>(`${this.apiUrl}/service/${id}/delete`);
  }

  loadTypes(): Observable<string[]>{
    return this.get<string[]>(`${this.apiUrl}/types`);
  }
  getAllServicesWithParameters(filter: string, sort: string): Observable<IService[]> {
    let query = `${this.apiUrl}/services?sort=${sort}`;
    if (filter != "") {
      query += `&filter=${filter}`;
    }
    return this.get<IService[]>(query);
  }
  getMaterial(params: Params): Observable<PaginationModel<IMaterial>> {
    return this.getWithOptions<PaginationModel<IMaterial>>(`${this.apiUrl}/materials`, {params: params});
  }

  getMaterialType(): Observable<IMaterialType[]> {
    return this.get<IMaterialType[]>(`${this.apiUrl}/material/get-material-type`);
  }

  getMeasurement(): Observable<IMeasurement[]> {
    return this.get<IMeasurement[]>(`${this.apiUrl}/material/get-measurement`);
  }
  deleteMaterial(id: number): Observable<number> {
    return this.delete<number>(`${this.apiUrl}/material/${id}`)
  }
  createMaterial(material: IMaterial): Observable<IMaterial> {
    return this.post<IMaterial>(`${this.apiUrl}/material/create`, material);
  }
  editMaterial(material: IMaterial): Observable<IMaterial> {
    return this.put<IMaterial>(`${this.apiUrl}/material/edit`, material);
  }
}


