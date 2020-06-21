import { AdditionalField } from './additionalField';

export class Type {
    id: string;
    topicId: string;
    name: string;
    additionalFields: AdditionalField[];

    constructor(id: string, topicId: string, name: string) {
        this.id = id;
        this.topicId = topicId;
        this.name = name;
    }
}
