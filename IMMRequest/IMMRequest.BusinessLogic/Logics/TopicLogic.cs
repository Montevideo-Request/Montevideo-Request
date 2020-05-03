using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using IMMRequest.Exceptions;
using System;

namespace IMMRequest.BusinessLogic
{
    public class TopicLogic : BaseLogic<Topic, Area>
    {
        public TopicLogic(IRepository<Topic, Area> topicRepository) 
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
            throw new ExceptionController(LogicExceptions.NOT_IMPLEMENTED);
        }

        public override void IsValid(Topic topic)
        { 
            if(topic.Name.Length == 0)
            {
                throw new ExceptionController(LogicExceptions.INVALID_LENGTH);
            }
            if(ContainsTopic(topic.Name, topic.AreaId))
            {
                throw new ExceptionController(LogicExceptions.ALREADY_EXISTS_TOPIC);
            }
        }

        public bool ContainsTopic(string name, Guid areaId)
        {
            bool containsType = false;
            Area area = this.repository.GetParent(areaId);
            Topic dummyTopic = new Topic();
            dummyTopic.Name = name;
            if(area.Topics.Contains(dummyTopic))
            {
                containsType = true;
            }
            return containsType;
        }

        public override void EntityExists(Guid id)
        {
            Topic dummyTopic = new Topic();
            dummyTopic.Id = id;
            
            if(!this.repository.Exist(dummyTopic))
            {
                throw new ExceptionController(LogicExceptions.INVALID_ID_TOPIC);
            }
        }
    }
}
