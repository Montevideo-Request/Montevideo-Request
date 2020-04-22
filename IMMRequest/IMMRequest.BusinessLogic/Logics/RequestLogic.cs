using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System;


namespace IMMRequest.BusinessLogic
{
    public class RequestLogic : BaseLogic<Request>
    {
		public RequestLogic(IRepository<Request> requestRepository) 
        {
            this.repository = requestRepository;
        }

        public RequestLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.repository = new RequestRepository(IMMRequestContext);
		}

        public override void Update(Request entity)
        {
            throw new NotImplementedException();
        }
    }
}
