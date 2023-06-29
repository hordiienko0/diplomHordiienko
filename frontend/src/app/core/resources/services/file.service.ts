import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

export class FileService {
  baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {
  }

  post<T>(path: string, formData: FormData) {
    return this.http.post<T>(`${this.baseUrl}${path}`, formData);
  }

  put<T>(path: string, formData: FormData) {
    return this.http.put<T>(`${this.baseUrl}${path}`, formData);
  }

  getDocumentDownloadUrl(path: string) {
    return `https://${environment.apiHost}/files/${path}`;
  }

  downloadFile(path: string, fileName: string) {
    let xhr = new XMLHttpRequest();
    xhr.open("GET", this.getDocumentDownloadUrl(path), true);
    xhr.responseType = "blob";
    xhr.onload = function (e) {
      if (this.status == 200) {
        const blob = this.response;
        const a = document.createElement("a");
        document.body.appendChild(a);
        const blobUrl = window.URL.createObjectURL(blob);
        a.href = blobUrl;
        a.download = fileName;
        a.click();
        setTimeout(() => {
          window.URL.revokeObjectURL(blobUrl);
          document.body.removeChild(a);
        }, 0);
      }
    };
    xhr.send();
  }
}
