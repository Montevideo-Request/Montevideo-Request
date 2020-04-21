using System;
using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System.Linq;
using IMMRequest.DataAccess.Interface;
using IMMRequest.BusinessLogic.Interface;

namespace IMMRequest.BusinessLogic 
{
    public class AdministratorLogic : BaseLogic<Administrator>
    {
        public IRepository<Administrator> administratorRepository;

		public AdministratorLogic(IRepository<Administrator> adminRepository) 
        {
            this.administratorRepository = adminRepository;
		}

        public AdministratorLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.administratorRepository = new AdministratorRepository(IMMRequestContext);
		}

        public Guid Create(Administrator administrator) 
        {   
            try {
                this.administratorRepository.Add(administrator);
                this.administratorRepository.Save();
                return administrator.Id;
            } 
            catch {
                throw new ArgumentException("Invalid guid");
            }
            
        }

        public override void Update(Administrator administrator) 
        {
            try
            {
                Administrator administratorToUpdate = this.administratorRepository.Get(administrator.Id);
                administratorToUpdate.Email = administrator.Email;
                administratorToUpdate.Name = administrator.Name;
                administratorToUpdate.Password = administrator.Password;
                this.administratorRepository.Update(administratorToUpdate);
                this.administratorRepository.Save();
            }
            catch
            {
                throw new ArgumentException("Invalid guid");
            }
        }
    }
}