using IMMRequest.DataAccess.Interface;
using IMMRequest.Exceptions;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System.Net.Mail;
using System;

namespace IMMRequest.BusinessLogic 
{
    public class AdministratorLogic : BaseLogic<Administrator, Administrator>
    {

		public AdministratorLogic(IRepository<Administrator, Administrator> adminRepository) 
        {
            this.repository = adminRepository;
		}

        public AdministratorLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.repository = new AdministratorRepository(IMMRequestContext);
		}

        public override void Update(Administrator administrator) 
        {
            NotExist(administrator.Id);
            Administrator administratorToUpdate = this.repository.Get(administrator.Id);
            administratorToUpdate.Email = administrator.Email;
            administratorToUpdate.Name = administrator.Name;
            administratorToUpdate.Password = administrator.Password;
            this.repository.Update(administratorToUpdate);
            this.repository.Save();
        }

        public override void Remove(Administrator administrator)
        {
            NotExist(administrator.Id);
            this.repository.Remove(administrator);
            this.repository.Save();
        }

        public override void IsValid(Administrator administrator)
        { 
            if(administrator.Email != null && administrator.Email.Length == 0)
            {
                throw new ExceptionController(LogicExceptions.EMPTY_EMAIL_INPUT);
            }
            if(administrator.Name != null && administrator.Name.Length == 0)
            {
                throw new ExceptionController(LogicExceptions.EMPTY_NAME_INPUT);
            }
            if(administrator.Password != null && administrator.Password.Length == 0)
            {
                throw new ExceptionController(LogicExceptions.EMPTY_PASSWORD_INPUT);
            }
            ValidEmailFormat(administrator.Email);
            EmailNotExist(administrator.Email);
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

        private void EmailNotExist(string email)
        {
            var administrator = this.repository.GetByCondition(a => a.Email == email);
            
            if(administrator != null)
            {
                throw new ExceptionController(LogicExceptions.INVALID_EMAIL_IN_USE);
            }
        }
        
        public override void EntityExist(Administrator administrator)
        {
            if(!this.repository.Exist(administrator))
            {
                throw new ExceptionController(LogicExceptions.INVALID_ID_ADMINISTRATOR);
            }
        }

        public override void NotExist(Guid id)
        {
            Administrator dummyAdministrator = new Administrator();
            dummyAdministrator.Id = id;
            if(!this.repository.Exist(dummyAdministrator)){
                throw new ExceptionController(LogicExceptions.INVALID_ID_ADMINISTRATOR);
            }
        }
    }
}
