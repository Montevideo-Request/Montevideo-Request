using System;
using IMMRequest.BusinessLogic.Interface;
using IMMRequest.DataAccess;

namespace IMMRequest.BusinessLogic
{
    public class SessionLogic : ISessionLogic
    {
        private readonly AdministratorRepository administratorRepository;
        
        public SessionLogic(AdministratorRepository _administratorRepository)
        {
            this.administratorRepository = _administratorRepository;
        }

        public Guid Login(string email, string password)
        {
            try
            {
                var administrator = this.administratorRepository.Get(s => s.Email == email && s.Password == password);

                if(administrator.Token == Guid.Empty)
                {
                    administrator.Token = Guid.NewGuid();

                    this.administratorRepository.Update(administrator);
                    this.administratorRepository.Save();
                }

                return administrator.Token;
            }
            catch(Exception)
            {
                throw new ExceptionController(ExceptionMessage.INVALID_CREDENTIALS);
            }
        }

        public bool IsValidToken(Guid token)
        {
            return this.administratorRepository.Exist(s => s.Token == token);
        }
    }
}