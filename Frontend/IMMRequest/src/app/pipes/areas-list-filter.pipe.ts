import { Pipe, PipeTransform } from '@angular/core';
import { Area } from '../models/area';

@Pipe({
  name: 'areasListFilter'
})
export class AreasListFilterPipe implements PipeTransform {

  transform(value: Area[], filterBy: string): Area[] {
    filterBy = filterBy ? filterBy.toLocaleLowerCase() : null;

    return filterBy ? value.filter((area: Area) =>
        area.name.toLocaleLowerCase().indexOf(filterBy) !== -1) : value;
  }
}
