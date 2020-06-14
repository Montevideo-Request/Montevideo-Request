import { Pipe, PipeTransform } from '@angular/core';
import { Administrator } from '../models/administrator';

@Pipe({
  name: 'administratorListFilter'
})
export class AdministratorListFilterPipe implements PipeTransform {

  transform(value: Administrator[], filterBy: string): Administrator[] {
    filterBy = filterBy ? filterBy.toLocaleLowerCase() : null;

    return filterBy ? value.filter((administrator: Administrator) =>
        administrator.Name.toLocaleLowerCase().indexOf(filterBy) !== -1) : value;
  }
}
