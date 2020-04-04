using System;

namespace IMMRequest.Domain 
{
    public class Administrator : Person 
    {
        public string Password { get; set; }

        public Administrator() {}

        public Administrator(string Name, string Email, string Password) 
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
        }

        public bool IsValid() 
        {
            return true;
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