export class FieldRange {
    id: string;
    additionalFieldId: string;
    range: string;

    constructor(id: string, additionalFieldId: string, range: string){
        this.id = id;
        this.additionalFieldId = additionalFieldId;
        this.range = range;
    }
}
