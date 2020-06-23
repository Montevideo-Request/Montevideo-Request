import { FieldRangeValue } from './FieldRangeValue';
import { Type } from './type'

export class Request {
  id: string;
  state: string;
  type: Type;
  date: Date;
  typeId: string;
  description: string;
  requestorsName: string;
  requestorsEmail: string;
  requestorsPhone: string;
  additionalFieldValues: FieldRangeValue[];

  constructor(id: string, typeId: string, description: string, requestorsName: string, type: Type, date: Date,
    requestorsEmail: string, requestorsPhone: string, additionalFieldValues: FieldRangeValue[]) {
    this.id = id;
    this.type = type;
    this.date = date;
    this.typeId = typeId;
    this.description = description;
    this.requestorsName = requestorsName;
    this.requestorsEmail = requestorsEmail;
    this.requestorsPhone = requestorsPhone;
    this.additionalFieldValues = additionalFieldValues;
  }
}
