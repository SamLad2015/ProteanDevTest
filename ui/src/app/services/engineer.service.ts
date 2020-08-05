import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EngineerService {
  baseApiUrl = environment.api;
  constructor(private httpClient: HttpClient) { }

  public GetEngineers(): Observable<string[]> {
    return this.httpClient.get<string[]>(`${this.baseApiUrl}/engineer`);
  }
}
