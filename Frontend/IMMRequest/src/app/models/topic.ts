import { Type } from './Type';

export class Topic {
    Id: string;
    Name: string;
    AreaId: string;
    Types: Type[];

    constructor(id: string, areaId: string, name: string, types: Type[]) {
        this.Id = id;
        this.AreaId = areaId;
        this.Name = name;
        this.Types = types;
    }
}
