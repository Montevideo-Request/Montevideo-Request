using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System;
using IMMRequest.DataAccess.Interface;


namespace IMMRequest.BusinessLogic
{
    public class TopicLogic : BaseLogic<Topic>
    {
        public IRepository<Topic> topicRepository;

        public TopicLogic(IRepository<Topic> topicRepository) 
        {
            this.topicRepository = topicRepository;
        }

        public TopicLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.topicRepository = new TopicRepository(IMMRequestContext);
		}

        public Guid Create(Topic topic) 
        {
            try {
                this.topicRepository.Add(topic);
                this.topicRepository.Save();
                return topic.Id;
            } 
            catch {
                throw new ArgumentException("Invalid guid");
            }
        }

        public override void Update(Topic entity)
        {
            throw new NotImplementedException();
        }
    }
}
