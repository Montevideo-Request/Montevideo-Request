import { Type } from './type';

export class Topic {
    id: string;
    name: string;
    areaId: string;
    types: Type[];

    constructor(id: string, areaId: string, name: string, types: Type[]) {
        this.id = id;
        this.areaId = areaId;
        this.name = name;
        this.types = types;
    }
}
