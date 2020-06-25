import { AdditionalFieldValue } from './additionalFieldValue';
import { Type } from './type'
import { Guid } from "guid-typescript";

export class Request {
  id: string;
  state: string;
  type: Type;
  date: Date;
  typeId: Guid;
  description: string;
  requestorsName: string;
  requestorsEmail: string;
  requestorsPhone: string;
  additionalFieldValues: AdditionalFieldValue[];

  constructor(id: string, typeId: Guid, description: string, requestorsName: string, type: Type, date: Date,
    requestorsEmail: string, requestorsPhone: string, additionalFieldValues: AdditionalFieldValue[]) {
    this.type = type;
    this.typeId = typeId;
    this.description = description;
    this.requestorsName = requestorsName;
    this.requestorsEmail = requestorsEmail;
    this.requestorsPhone = requestorsPhone;
    this.additionalFieldValues = additionalFieldValues;
  }
}
