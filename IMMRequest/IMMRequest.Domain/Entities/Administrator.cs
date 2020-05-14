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

        public Administrator() {
            this.Id = Guid.NewGuid();
        }

        public Administrator(string Name, string Email, string Password) 
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
            this.Token = Guid.NewGuid();
        }

        public override bool Equals(Object obj) 
        {
			Administrator administrator = obj as Administrator;
			bool equals = false;
			if (obj == null) {
				equals = false;
			}
			else {
				equals = this.Email == administrator.Email && this.Name == administrator.Name;
			}
			return equals;
		}

    }
}
