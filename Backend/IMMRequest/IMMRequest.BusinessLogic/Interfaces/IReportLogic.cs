using System.Collections.Generic;
using IMMRequest.Domain;

namespace IMMRequest.BusinessLogic
{
    public interface IReportLogic
    {
        IEnumerable<Request> GetFilteredRequests(string email, string from, string to);
        string GetFilteredTypes(string from, string to);
    }
}
