import { FieldRangeValue } from './FieldRangeValue';

export class Request {
  Id: number;
  State: string;
  TypeId: number;
  Description: string;
  RequestorsName: string;
  RequestorsEmail: string;
  RequestorsPhone: string;
  AdditionalFieldValues: FieldRangeValue[];
}
