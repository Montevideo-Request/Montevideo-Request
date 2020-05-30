using IMMRequest.DataAccess;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
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

        public override Topic Update(Topic topic)
        {
            NotExist(topic.Id);

            Topic topicToUpdate = this.repository.Get(topic.Id);

            IsValidToUpdate(topic, topicToUpdate);
            
            topicToUpdate.Name = topic.Name;

            this.repository.Update(topicToUpdate);
            this.repository.Save();

            return topicToUpdate;
        }

        public override void Remove(Guid id)
        {
            NotExist(id);
            Topic topicToDelete = this.repository.Get(id);
            this.repository.Remove(topicToDelete);
            this.repository.Save();
        }

        public override void IsValid(Topic topic)
        { 
            if((topic.Name != null && topic.Name.Length == 0 )|| topic.Name == null)
            {
                throw new ExceptionController(LogicExceptions.INVALID_NAME);
            }
            if(ContainsTopic(topic.Name, topic.AreaId))
            {
                throw new ExceptionController(LogicExceptions.ALREADY_EXISTS_TOPIC);
            }
        }

        public void IsValidToUpdate(Topic topic, Topic topicToUpdate)
        { 
            if( (topic.Name != null && topic.Name.Length == 0) || topic.Name == null )
            {
                throw new ExceptionController(LogicExceptions.INVALID_NAME);
            }

            if (topic.AreaId == null || (topic.AreaId != null && topic.AreaId.ToString().Length == 0))
            {
                throw new ExceptionController(LogicExceptions.INVALID_ID_AREA);  
            }

            if (topicToUpdate.AreaId != topic.AreaId)
            {
                throw new ExceptionController(LogicExceptions.INVALID_AREA_TOPIC_COMBINATION);  
            }

            if (this.repository.NameExists(topic))
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

        public override void EntityExist(Topic entity)
        {
            if(this.repository.Exist(entity))
            {
                throw new ExceptionController(LogicExceptions.ALREADY_EXISTS_TOPIC);
            }
        }

        public override void NotExist(Guid id)
        {
            Topic dummyTopic = new Topic();
            dummyTopic.Id = id;
            if(!this.repository.Exist(dummyTopic)){
                throw new ExceptionController(LogicExceptions.INVALID_ID_TOPIC);
            }
        }
    }
}
