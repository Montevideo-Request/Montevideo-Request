using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using IMMRequest.BusinessLogic.Interface;
using System.Net.Mail;

namespace IMMRequest.BusinessLogic 
{
    public class AdministratorLogic : BaseLogic<Administrator>
    {

		public AdministratorLogic(IRepository<Administrator> adminRepository) 
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
                throw new ExceptionController(ExceptionMessage.INVALID_ID);
            }
        }

        public override void IsValid(Administrator administrator)
        { 
            ValidEmailFormat(administrator.Email);
            NotExist(administrator.Email);
        }

        private void ValidEmailFormat(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
            }
            catch
            {
                throw new ExceptionController(ExceptionMessage.INVALID_EMAIL_FORMAT);
            }
        }

        private void NotExist(string email)
        {
            if (repository.Exist(a => a.Email == email))
            {
                throw new ExceptionController(ExceptionMessage.EMAIL_IN_USE);
            }
        }
    }
}
