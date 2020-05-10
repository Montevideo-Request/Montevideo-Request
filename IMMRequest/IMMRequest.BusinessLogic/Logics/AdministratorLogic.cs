using IMMRequest.BusinessLogic.Interface;
using IMMRequest.DataAccess.Interface;
using System.Collections.Generic;
using IMMRequest.Exceptions;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System.Net.Mail;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic 
{
    public class AdministratorLogic : IAdministratorLogic<Administrator>
    {
        protected IAdministratorRepository<Administrator> repository;
		public AdministratorLogic(IAdministratorRepository<Administrator> adminRepository) 
        {
            this.repository = adminRepository;
		}

        public AdministratorLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.repository = new AdministratorRepository(IMMRequestContext);
		}

        public Administrator Create(Administrator entity)
        {
            IsValid(entity);
            this.repository.Add(entity);
            this.repository.Save();
            return entity;
        }

        public Administrator Get(Guid id)
        {
            NotExist(id);
            return this.repository.Get(id);
        }

        public IEnumerable<Administrator> GetAll()
        {
            IEnumerable<Administrator> entities = this.repository.GetAll();
            
            if (entities.Count() == 0) 
            {
                throw new ExceptionController(LogicExceptions.GENERIC_NO_ELEMENTS);
            }

            return entities;
		}

        public void Save()
        {
            this.repository.Save();
        }

        public Administrator Update(Administrator administrator) 
        {
            NotExist(administrator.Id);
            Administrator administratorToUpdate = this.repository.Get(administrator.Id);
            administratorToUpdate.Email = administrator.Email != null ? administrator.Email : administratorToUpdate.Email;
            administratorToUpdate.Name = administrator.Name != null ? administrator.Name : administratorToUpdate.Name;
            administratorToUpdate.Password = administrator.Password != null ? administrator.Password : administratorToUpdate.Password;
            this.repository.Update(administratorToUpdate);
            this.repository.Save();

            return administratorToUpdate;
        }

        public void Remove(Guid id)
        {
            NotExist(id);
            Administrator adminToDelete = this.repository.Get(id);
            this.repository.Remove(adminToDelete);
            this.repository.Save();
        }

        public void IsValid(Administrator administrator)
        { 
            if((administrator.Email != null && administrator.Email.Length == 0) || administrator.Email == null )
            {
                throw new ExceptionController(LogicExceptions.EMPTY_EMAIL_INPUT);
            }
            if((administrator.Name != null && administrator.Name.Length == 0) || administrator.Name == null )
            {
                throw new ExceptionController(LogicExceptions.EMPTY_NAME_INPUT);
            }
            if((administrator.Password != null && administrator.Password.Length == 0)  || administrator.Password == null )
            {
                throw new ExceptionController(LogicExceptions.EMPTY_PASSWORD_INPUT);
            }

            ValidEmailFormat(administrator.Email);
            EntityExist(administrator);
        }

        private void ValidEmailFormat(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
            }
            catch
            {
                throw new ExceptionController(LogicExceptions.INVALID_EMAIL_FORMAT);
            }
        }
        
        public void EntityExist(Administrator administrator)
        {
            if(this.repository.Exist(administrator))
            {
                throw new ExceptionController(LogicExceptions.ALREADY_EXISTS_ADMIN);
            }
        }

        public void NotExist(Guid id)
        {
            Administrator dummyAdministrator = new Administrator();
            dummyAdministrator.Id = id;
            if(!this.repository.Exist(id))
            {
                throw new ExceptionController(LogicExceptions.INVALID_ADMINISTRATOR);
            }
        }
    }
}
