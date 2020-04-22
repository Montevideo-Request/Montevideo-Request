using IMMRequest.Domain;
using System;

namespace IMMRequest.WebApi.Models
{
    public class AdministratorModel : Model<Administrator, AdministratorModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public AdministratorModel() { }

        public AdministratorModel(Administrator entity)
        {
            SetModel(entity);
        }

        public override Administrator ToEntity()  => new Administrator()
        {
            Id = this.Id,
            Name = this.Name,
            Email = this.Email,
            Password = this.Password
        };

        protected override AdministratorModel SetModel(Administrator entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Email = entity.Email;
            return this;
        }
    }
}
