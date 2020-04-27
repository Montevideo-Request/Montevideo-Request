using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using IMMRequest.BusinessLogic.Interface;

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
            try
            {
                Request requestToUpdate = this.repository.Get(entity.Id);
                requestToUpdate.RequestorsEmail = entity.RequestorsEmail;
                requestToUpdate.RequestorsName = entity.RequestorsName;
                requestToUpdate.RequestorsPhone = entity.RequestorsPhone;
                this.repository.Update(requestToUpdate);
                this.repository.Save();
            }
            catch
            {
                throw new ExceptionController(ExceptionMessage.INVALID_ID);
            } 
        }

        public override void IsValid(Request request)
        { 
            return ;
        }
    }
}
