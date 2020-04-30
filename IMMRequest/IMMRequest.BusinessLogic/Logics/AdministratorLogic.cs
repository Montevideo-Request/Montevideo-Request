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
            try
            {
                Administrator administratorToUpdate = this.repository.Get(administrator.Id);
                administratorToUpdate.Email = administrator.Email;
                administratorToUpdate.Name = administrator.Name;
                administratorToUpdate.Password = administrator.Password;
                this.repository.Update(administratorToUpdate);
                this.repository.Save();
            }
            catch
            {
                throw new ExceptionController(LogicExceptions.INVALID_ID);
            }
        }

        public override void IsValid(Administrator administrator)
        { 
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
            if (repository.Exist(a => a.Email == email))
            {
                throw new ExceptionController(LogicExceptions.EMAIL_IN_USE);
            }
        }

        
        public override void EntityExists(Guid id)
        {
            if (!repository.Exist(a => a.Id == id))
            {
                throw new ExceptionController(LogicExceptions.INVALID_ADMINISTRATOR_ID);
            }
        }
    }
}
