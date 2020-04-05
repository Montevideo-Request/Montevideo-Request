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

        public void Remove(Guid id) 
        {
            try 
            {
                Administrator administrator = this.administratorRepository.Get(id);
                this.administratorRepository.Remove(administrator);
                this.administratorRepository.Save();
            }
            catch
            {
                throw new ArgumentException("Invalid Id");
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
                throw new ArgumentException("Invalid Id");
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