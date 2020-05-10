using System;

namespace IMMRequest.BusinessLogic.Interface
{
    public interface ISessionLogic
    {
        Guid Login(string email, string password);
        bool IsValidToken(Guid token);
    }
}