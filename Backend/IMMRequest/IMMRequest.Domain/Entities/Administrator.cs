using System;

namespace IMMRequest.Domain 
{
    public class Administrator
    {
        public Guid Id {get; set;}
        
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Guid Token { get; set; }
        public Boolean IsDeleted { get; set; }

        public Administrator() {
            this.Id = Guid.NewGuid();
            this.Token = Guid.NewGuid();
            this.IsDeleted = false;
        }

        public Administrator(string Name, string Email, string Password) 
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
            this.Token = Guid.NewGuid();
            this.IsDeleted = false;
        }
    }
}
