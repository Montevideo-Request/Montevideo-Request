import { FieldRange } from './FieldRange';

export class AdditionalField {
  id: string;
  typeId: string;
  name: string;
  fieldType: string;
  multiSelect: boolean;
  ranges: FieldRange[];

  constructor(id: string, typeId: string, name: string, fieldType: string, multiSelect: boolean, ranges: FieldRange[]) {
    this.id = id;
    this.typeId = typeId;
    this.name = name;
    this.fieldType = fieldType;
    this.multiSelect = multiSelect;
    this.ranges = ranges;
  }
}
