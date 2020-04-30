using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using IMMRequest.BusinessLogic.Interface;
using System;
using System.Collections.Generic;

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

        public override void IsValid(Topic topic)
        { 
            if(topic.Name.Length == 0)
            {
                throw new ExceptionController(ExceptionMessage.INVALID_LENGTH);
            }
            if(ContainsTopic(topic.Name, topic.AreaId))
            {
                throw new ExceptionController(ExceptionMessage.TOPIC_ALREADY_EXISTS);
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
                throw new ExceptionController(ExceptionMessage.TOPIC_ALREADY_EXISTS);
            }
        }

        public override void EntityExists(Guid id)
        {
            if (!repository.Exist(a => a.Id == id))
            {
                throw new ExceptionController(ExceptionMessage.INVALID_TOPIC_ID);
            }
        }
    }
}
