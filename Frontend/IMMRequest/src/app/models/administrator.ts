export class Administrator {
    Id: string;
    Name: string;
    Email: string;
    Password: string;

    constructor(id: string, name: string, email: string, password: string) {
        this.Id = id;
        this.Name = name;
        this.Email = email;
        this.Password = password;
    }
}