import { FieldRangeValue } from './FieldRangeValue';

export class Request {
  id: string;
  state: string;
  typeId: string;
  description: string;
  requestorsName: string;
  requestorsEmail: string;
  requestorsPhone: string;
  additionalFieldValues: FieldRangeValue[];

  constructor(id: string, typeId: string, description: string, requestorsName: string,
    requestorsEmail: string, requestorsPhone: string, additionalFieldValues: FieldRangeValue[]) {
    this.id = id;
    this.typeId = typeId;
    this.description = description;
    this.requestorsName = requestorsName;
    this.requestorsEmail = requestorsEmail;
    this.requestorsPhone = requestorsPhone;
    this.additionalFieldValues = additionalFieldValues;
  }
}
