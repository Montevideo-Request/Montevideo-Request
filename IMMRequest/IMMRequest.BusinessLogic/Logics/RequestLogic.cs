using System;
using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System.Linq;
using IMMRequest.BusinessLogic.Interface;

namespace IMMRequest.BusinessLogic
{
    public class RequestLogic : ILogic<Request>
    {
        public RequestRepository requestRepository;

        public RequestLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.requestRepository = new RequestRepository(IMMRequestContext);
		}

        public void Add(Request request)
        {
            this.requestRepository.Add(request);
        }

        public void Save()
        {
            this.requestRepository.Save();
        }        

        public Request Create(Request request) 
        {
            try
            {
                this.Add(request);
                return request;
            } 
            catch 
            {
                throw new ArgumentException("Id already exists");
            }
        }

        public void Remove(Guid id) 
        {
            try 
            {
                Request request = this.requestRepository.Get(id);
                this.requestRepository.Remove(request);
                this.requestRepository.Save();
            }
            catch
            {
                throw new ArgumentException("Invalid Id");
            }
        }

		public Request Get(Guid id) 
        {
            try
            {
                return this.requestRepository.Get(id);
            }
            catch 
            {
                throw new ArgumentException("Invalid Id");
            }
        }

        public IEnumerable<Request> GetAll() 
        {
            IEnumerable<Request> requests = this.requestRepository.GetAll();
            
            if (requests.Count() == 0) 
            {
                throw new ArgumentException("There are no Requests");
            }

            return requests;
		}

        public void Update(Request entity)
        {
            throw new NotImplementedException();
        }
    }
}