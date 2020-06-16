import { Pipe, PipeTransform } from '@angular/core';
import { AdministratorBasicInfo } from '../models/administratorBasicInfo';

@Pipe({
  name: 'administratorListFilter'
})
export class AdministratorListFilterPipe implements PipeTransform {

  transform(value: AdministratorBasicInfo[], filterBy: string): AdministratorBasicInfo[] {
    filterBy = filterBy ? filterBy.toLocaleLowerCase() : null;

    return filterBy ? value.filter((administrator: AdministratorBasicInfo) =>
        administrator.name.toLocaleLowerCase().indexOf(filterBy) !== -1) : value;
  }
}
