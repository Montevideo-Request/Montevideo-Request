import { Pipe, PipeTransform } from '@angular/core';
import { Topic } from '../models/Topic';

@Pipe({
  name: 'topicsListFilter'
})
export class TopicsListFilterPipe implements PipeTransform {

  transform(value: Topic[], filterBy: string): Topic[] {
    filterBy = filterBy ? filterBy.toLocaleLowerCase() : null;

    return filterBy ? value.filter((topic: Topic) =>
        topic.name.toLocaleLowerCase().indexOf(filterBy) !== -1) : value;
  }
}
