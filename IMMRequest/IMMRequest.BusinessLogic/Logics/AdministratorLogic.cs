using  System;
using  System.Collections.Generic;
using  IMMRequest.DataAccess;
using  IMMRequest.Domain;

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
		public Administrator Get(Guid id) 
        {
            try
            {
                return this.administratorRepository.Get(id);
            }
            catch {
                throw new ArgumentException("Invalid guid");
            }
        }

        public IEnumerable<Administrator> GetAdministrators() 
        {
			return this.administratorRepository.GetAll();
		}
    }
}