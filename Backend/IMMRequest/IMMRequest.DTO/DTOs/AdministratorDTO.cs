using IMMRequest.Domain;
using System;

namespace IMMRequest.DTO
{
    public class AdministratorDTO : DTO<Administrator, AdministratorDTO>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public AdministratorDTO() { }

        public AdministratorDTO(Administrator entity)
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

        protected override AdministratorDTO SetModel(Administrator entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Email = entity.Email;
            return this;
        }
    }
}
