import {Injectable} from "@angular/core";
import {FileService} from "../../../../core/resources/services/file.service";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";


@Injectable({
  providedIn: AdministrationFileService,
})
export class AdministrationFileService extends FileService {
  ApiPath: string = '';

  constructor(http: HttpClient) {
    super(http);
  }

   postNewMembers(data: { companyId: number, file: File }): Promise<string[]|undefined> {
    const formData = new FormData();
    formData.append('file', data.file, data.file.name);
    formData.append('companyId', data.companyId.toString())
    return this.post<string[]>('/users/upload', formData).toPromise();
  }
}
