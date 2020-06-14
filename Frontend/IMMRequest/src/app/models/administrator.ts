export class Administrator {
    Id: number;
    Name: string;
    Email: string;

    constructor(id: number, name: string, email: string) {
        this.Id = id;
        this.Name = name;
        this.Email = email;
    }
}