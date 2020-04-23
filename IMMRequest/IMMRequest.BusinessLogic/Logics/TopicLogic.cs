using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using IMMRequest.Exceptions;

namespace IMMRequest.BusinessLogic
{
    public class TopicLogic : BaseLogic<Topic>
    {
        public TopicLogic(IRepository<Topic> topicRepository) 
        {
            this.repository = topicRepository;
        }

        public TopicLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.repository = new TopicRepository(IMMRequestContext);
		}

        public override void Update(Topic entity)
        {
            throw new ExceptionController(ExceptionMessage.NOT_IMPLEMENTED);
        }
    }
}
