import { FieldRange } from './FieldRange';

export class AdditionalField {
  Id: string;
  TypeId: string;
  Name: string;
  FieldType: string;
  MultiSelect: boolean;
  Ranges: FieldRange[];

  constructor(id: string, typeId: string, name: string, fieldType: string, multiSelect: boolean, ranges: FieldRange[]) {
    this.Id = id;
    this.TypeId = typeId;
    this.Name = name;
    this.FieldType = fieldType;
    this.MultiSelect = multiSelect;
    this.Ranges = ranges;
  }
}
