import { FieldRangeValue } from './FieldRangeValue';

export class Request {
  Id: string;
  State: string;
  TypeId: string;
  Description: string;
  RequestorsName: string;
  RequestorsEmail: string;
  RequestorsPhone: string;
  AdditionalFieldValues: FieldRangeValue[];

  constructor(id: string, typeId: string, description: string, requestorsName: string,
    requestorsEmail: string, requestorsPhone: string, additionalFieldValues: FieldRangeValue[]) {
    this.Id = id;
    this.TypeId = typeId;
    this.Description = description;
    this.RequestorsName = requestorsName;
    this.RequestorsEmail = requestorsEmail;
    this.RequestorsPhone = requestorsPhone;
    this.AdditionalFieldValues = additionalFieldValues;
  }
}
