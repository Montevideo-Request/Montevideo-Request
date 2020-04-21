using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System;
using IMMRequest.DataAccess.Interface;

namespace IMMRequest.BusinessLogic
{
    public class RequestLogic : BaseLogic<Request>
    {
        public IRepository<Request> requestRepository;

		public RequestLogic(IRepository<Request> requestRepository) 
        {
            this.requestRepository = requestRepository;
        }

        public RequestLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.requestRepository = new RequestRepository(IMMRequestContext);
		}

        public Guid Create(Request request) 
        {
            try {
                this.requestRepository.Add(request);
                this.requestRepository.Save();
                return request.Id;
            } 
            catch {
                throw new ArgumentException("Invalid guid");
            }
        }

        public override void Update(Request entity)
        {
            throw new NotImplementedException();
        }
    }
}