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
                throw new ExceptionController(LogicExceptions.TOPIC_ALREADY_EXISTS);
            }
            NotExist(topic.Name);
        }

        public bool ContainsTopic(string name, Guid areaId)
        {
            bool containsType = false;
            Area area = this.repository.GetParent(areaId);
            Topic dummyTopic = new Topic();
            dummyTopic.Name = name;
            if(!area.Topics.Contains(dummyTopic))
            {
                containsType = true;
            }
            return containsType;
        }

        private void NotExist(string name)
        {
            if (repository.Exist(a => a.Name == name))
            {
                throw new ExceptionController(LogicExceptions.TOPIC_ALREADY_EXISTS);
            }
        }

        public override void EntityExists(Guid id)
        {
            if (!repository.Exist(a => a.Id == id))
            {
                throw new ExceptionController(LogicExceptions.INVALID_TOPIC_ID);
            }
        }
    }
}
