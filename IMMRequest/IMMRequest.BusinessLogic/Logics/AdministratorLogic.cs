using  System;
using  System.Collections.Generic;
using  IMMRequest.DataAccess;
using  IMMRequest.Domain;
using System.Linq;

namespace IMMRequest.BusinessLogic 
{
    public class AdministratorLogic 
    {
        public AdministratorRepository administratorRepository;

		public AdministratorLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.administratorRepository = new AdministratorRepository(IMMRequestContext);
		}

        public Administrator Create(Administrator administrator) 
        {
            try
            {
                this.administratorRepository.Add(administrator);
                this.administratorRepository.Save();
                return administrator;
            } 
            catch 
            {
                throw new ArgumentException("Id already exists");
            }
        }

		public Administrator Get(Guid id) 
        {
            try
            {
                return this.administratorRepository.Get(id);
            }
            catch 
            {
                throw new ArgumentException("Invalid guid");
            }
        }

        public IEnumerable<Administrator> GetAdministrators() 
        {
            IEnumerable<Administrator> administrators = this.administratorRepository.GetAll();
            
            if (administrators.Count() == 0) 
            {
                throw new ArgumentException("There are no Administrators");
            }

            return administrators;
		}
    }
}