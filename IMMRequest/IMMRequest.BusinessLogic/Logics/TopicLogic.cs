using  System;
using  System.Collections.Generic;
using  IMMRequest.DataAccess;
using  IMMRequest.Domain;
using System.Linq;
using IMMRequest.BusinessLogic.Interface;

namespace IMMRequest.BusinessLogic
{
    public class TopicLogic : ILogic<Topic>
    {
        public TopicRepository topicRepository;

        public TopicLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.topicRepository = new TopicRepository(IMMRequestContext);
		}

        public void Add(Topic topic)
        {
            this.topicRepository.Add(topic);
        }

        public void Save()
        {
            this.topicRepository.Save();
        }        

        public Topic Create(Topic topic) 
        {
            try
            {
                this.Add(topic);
                return topic;
            } 
            catch 
            {
                throw new ArgumentException("Id already exists");
            }
        }

        public void Remove(Topic topic) 
        {
            try 
            {
                this.topicRepository.Remove(topic);
                this.topicRepository.Save();
            }
            catch
            {
                throw new ArgumentException("Invalid Id");
            }
        }

		public Topic Get(Guid id) 
        {
            try
            {
                return this.topicRepository.Get(id);
            }
            catch 
            {
                throw new ArgumentException("Invalid Id");
            }
        }

        public IEnumerable<Topic> GetAll() 
        {
            IEnumerable<Topic> topics = this.topicRepository.GetAll();
            
            if (topics.Count() == 0) 
            {
                throw new ArgumentException("There are no Requests");
            }

            return topics;
		}

        public void Update(Topic entity)
        {
            throw new NotImplementedException();
        }
    }
}
