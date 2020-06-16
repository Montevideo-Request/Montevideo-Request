import { Guid } from 'guid-typescript';

export class AdministratorBasicInfo {
    id: string;
    name: string;
    email: string;

    constructor(id: string, name: string, email: string) {
        this.name = name;
        this.email = email;
    }
}
