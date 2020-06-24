using System.Collections.Generic;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
using Newtonsoft.Json;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic
{
    public class ReportLogic: IReportLogic
    {
        private readonly IRequestLogic<Request, TypeEntity> RequestLogic;
        private readonly ILogic<TypeEntity> TypeLogic;
        public ReportLogic(IRequestLogic<Request, TypeEntity> RequestLogic, ILogic<TypeEntity> TypeLogic) 
        { this.RequestLogic = RequestLogic; this.TypeLogic = TypeLogic; }
        public IEnumerable<Request> GetFilteredRequests(string email, string from, string to)
        {
            IEnumerable<Request> filteredRequests = new List<Request>();
            try
            {
                DateTime dateFrom = Convert.ToDateTime(from);
                DateTime dateTo = Convert.ToDateTime(to);

                filteredRequests = this.RequestLogic.GetAll().ToList()
                .Where(x => (x.Date.Date >= dateFrom.Date) && (x.Date.Date <= dateTo.Date) && (x.RequestorsEmail == email));
            }
            catch (System.Exception)
            {
                throw new ExceptionController(LogicExceptions.WRONG_DATE_FORMAT);
            }
            return filteredRequests;
        }

        public string GetFilteredTypes(string from, string to)
        {
            List<TypeEntity> filteredTypes = new List<TypeEntity>();
            IEnumerable<Request> requests = new List<Request>();
            List<TypeJSON> response = new List<TypeJSON>();
            
            try
            {
                DateTime dateFrom = Convert.ToDateTime(from);
                DateTime dateTo = Convert.ToDateTime(to);

                requests = this.RequestLogic.GetAll().ToList()
                .Where(x => (x.Date.Date >= dateFrom.Date) && (x.Date.Date <= dateTo.Date));

                foreach (Request request in requests)
                {
                    if (!this.TypeLogic.Exists(request.TypeId))
                    {
                        continue;
                    }
                    var typeInRequest = this.TypeLogic.Get(request.TypeId);
                    filteredTypes.Add(typeInRequest);
                }
            }
            catch (System.Exception)
            {
                throw new ExceptionController(LogicExceptions.WRONG_DATE_FORMAT);
            }

            var groupedTypes = filteredTypes.GroupBy( x => x.Id);
            foreach (var result in groupedTypes)
            {  
                TypeJSON json = new TypeJSON(result.Count(), filteredTypes.First(x => x.Id == result.Key));
                response.Add(json);
            }
            
            return JsonConvert.SerializeObject(response.ToList());
        }

        private class TypeJSON
        {
            public int quantity { get; set; }   
            public TypeEntity type { get; set; }
            public TypeJSON(int qty , TypeEntity type)
            {
                this.quantity = qty;
                this.type = type;
            }
        }
    }
}
