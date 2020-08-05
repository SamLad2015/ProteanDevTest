export interface CustomerModel {
  customerId: number;
  name: string;
  customerTypeId: number;
  customerType: string;
}

export interface CustomerTypeModel {
  customerTypeId: number;
  description: string;
}
