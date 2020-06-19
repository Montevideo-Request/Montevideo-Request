import { Topic } from './Topic';

export class Area {
    Id: string;
    Name: string;
    Topics: Topic[];

    constructor(id: string, name: string) {
        this.Id = id;
        this.Name = name;
    }
}
