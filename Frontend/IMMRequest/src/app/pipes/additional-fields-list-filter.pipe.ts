import { Pipe, PipeTransform } from '@angular/core';
import { AdditionalField } from '../models/additionalField';

@Pipe({
  name: 'additionalFieldsListFilter'
})
export class AdditionalFieldsListFilterPipe implements PipeTransform {

  transform(value: AdditionalField[], filterBy: string): AdditionalField[] {
    filterBy = filterBy ? filterBy.toLocaleLowerCase() : null;

    return filterBy ? value.filter((additionalField: AdditionalField) =>
        additionalField.name.toLocaleLowerCase().indexOf(filterBy) !== -1) : value;
  }
}
