import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FileService } from 'src/app/core/resources/services/file.service';

@Injectable({
  providedIn: 'root',
})
export class CompanyFileService extends FileService {
  ApiPath: string = `/companies`;

  constructor(http: HttpClient) {
    super(http);
  }

  putLogo(companyId: number, file: File) {
    let formData = new FormData();
    formData.append('data', file, file.name);
    return this.put<any>(`${this.ApiPath}/${companyId}/logo`, formData);
  }
}
