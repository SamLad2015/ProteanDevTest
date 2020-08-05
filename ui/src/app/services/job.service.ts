import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JobModel } from '../models/job.model';
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class JobService {
  baseApiUrl = environment.api;
  constructor(private httpClient: HttpClient) { }

  public GetJobs(): Observable<JobModel[]> {
    return this.httpClient.get<JobModel[]>(`${this.baseApiUrl}/job`);
  }

  public GetJob(jobId: number): Observable<JobModel> {
    return this.httpClient.get<JobModel>(`${this.baseApiUrl}/job/${jobId}`);
  }

  public CreateJob(job: JobModel): Promise<object> {
    job.customerId = parseInt(job.customerId.toString());
    return this.httpClient.post(`${this.baseApiUrl}/job`, job).toPromise();
  }
}
