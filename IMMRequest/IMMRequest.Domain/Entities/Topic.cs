using System.Collections.Generic;

namespace IMMRequest.Domain
{
    public class Topic
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public List<TopicType> Types { get; set; }

        public Topic() 
        {
            this.Types = new List<TopicType>();
        }
    }
}
