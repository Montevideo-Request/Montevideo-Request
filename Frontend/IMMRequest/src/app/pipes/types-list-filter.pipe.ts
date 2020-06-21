import { Pipe, PipeTransform } from '@angular/core';
import { Type } from '../models/type';

@Pipe({
  name: 'typesListFilter'
})
export class TypesListFilterPipe implements PipeTransform {

  transform(value: Type[], filterBy: string): Type[] {
    filterBy = filterBy ? filterBy.toLocaleLowerCase() : null;

    return filterBy ? value.filter((type: Type) =>
        type.name.toLocaleLowerCase().indexOf(filterBy) !== -1) : value;
  }

}
