import { FieldRange } from './FieldRange';

export class AdditionalField {
  Id: number;
  TypeId: number;
  Name: string;
  FieldType: string;
  MultiSelect: Boolean;
  Ranges: FieldRange[];
}