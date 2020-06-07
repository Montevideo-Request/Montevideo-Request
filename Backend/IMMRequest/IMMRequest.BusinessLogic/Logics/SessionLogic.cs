using IMMRequest.Exceptions;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System;

namespace IMMRequest.BusinessLogic
{
    public class SessionLogic : ISessionLogic
    {
        private readonly IAdministratorRepository<Administrator> administratorRepository;
        
        public SessionLogic(IAdministratorRepository<Administrator> _administratorRepository)
        {
            this.administratorRepository = _administratorRepository;
        }

        public Guid Login(string email, string password)
        {
            try
            {
                var administrator = this.administratorRepository.GetByCredentials(s => s.Email == email && s.Password == password && !s.IsDeleted);

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
                throw new ExceptionController(LogicExceptions.INVALID_CREDENTIALS);
            }
        }

        public bool IsValidToken(Guid token)
        {
            return this.administratorRepository.TokenExists(token);
        }
    }
}
