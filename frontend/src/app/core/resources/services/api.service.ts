import { HttpClient, HttpEvent } from '@angular/common/http';
import { environment } from "../../../../environments/environment";
import { Observable } from "rxjs";

export class ApiService {
  baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {
  }

  get<T>(path: string) {
    return this.http.get<T>(`${this.baseUrl}${path}`);
  }

  getWithOptions<T>(path: string, options: {}) {
    return this.http.get<T>(`${this.baseUrl}${path}`, options);
  }

  post<T>(path: string, data: any) {
    return this.http.post<T>(`${this.baseUrl}${path}`, data);
  }

  put<T>(path: string, body: any | null) {
    return this.http.put<T>(`${this.baseUrl}${path}`, body);
  }
  delete<T>(path: string) {
    return this.http.delete<T>(`${this.baseUrl}${path}`);
  }

  putWithOptions<T>(path: string, body: any | null, options?: any) {
    return this.http.put<T>(`${this.baseUrl}${path}`, body, options);
  }
}
