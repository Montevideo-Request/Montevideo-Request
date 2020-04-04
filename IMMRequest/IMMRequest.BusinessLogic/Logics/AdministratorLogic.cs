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
		public Administrator Get(int id) 
        {
            return null;
        }
    }
}