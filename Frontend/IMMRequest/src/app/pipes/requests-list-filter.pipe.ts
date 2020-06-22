import { Pipe, PipeTransform } from '@angular/core';
import { Request } from '../models/request';

@Pipe({
  name: 'requestsListFilter'
})
export class RequestsListFilterPipe implements PipeTransform {

  transform(value: Request[], filterBy: string): Request[] {
    filterBy = filterBy ? filterBy.toLocaleLowerCase() : null;

    return filterBy ? value.filter((request: Request) =>
        request.requestorsEmail.toLocaleLowerCase().indexOf(filterBy) !== -1) : value;
  }
}
