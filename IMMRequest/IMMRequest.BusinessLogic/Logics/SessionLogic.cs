using System;
using IMMRequest.BusinessLogic.Interface;
using IMMRequest.Exceptions;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using IMMRequest.DataAccess.Interface;

namespace IMMRequest.BusinessLogic
{
    public class SessionLogic : ISessionLogic
    {
        private readonly IRepository<Administrator, Administrator> administratorRepository;
        
        public SessionLogic(IRepository<Administrator, Administrator> _administratorRepository)
        {
            this.administratorRepository = _administratorRepository;
        }

        public Guid Login(string email, string password)
        {
            try
            {
                var administrator = this.administratorRepository.GetByCondition(s => s.Email == email && s.Password == password);

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
            Administrator dummyAdministrator = new Administrator();
            dummyAdministrator.Token = token;
            return this.administratorRepository.Exist(dummyAdministrator);
        }
    }
}