import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CustomerModel, CustomerTypeModel } from '../models/customer.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  baseApiUrl = environment.api;
  constructor(private httpClient: HttpClient) { }

  public GetCustomers(): Observable<CustomerModel[]> {
    return this.httpClient.get<CustomerModel[]>(`${this.baseApiUrl}/customer`);
  }

  public GetCustomerTypes(): Observable<CustomerTypeModel[]> {
    return this.httpClient.get<CustomerTypeModel[]>(`${this.baseApiUrl}/customer/types`);
  }

  public GetCustomer(customerId: number): Observable<CustomerModel> {
    return this.httpClient.get<CustomerModel>(`${this.baseApiUrl}/customer/${customerId}`);
  }

  public CreateCustomer(customer: CustomerModel): Promise<object> {
    customer.customerTypeId = parseInt(customer.customerTypeId.toString());
    return this.httpClient.post(`${this.baseApiUrl}/customer`, customer).toPromise();
  }
}
