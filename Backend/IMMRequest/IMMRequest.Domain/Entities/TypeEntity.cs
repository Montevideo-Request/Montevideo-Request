using System.Collections.Generic;
using System;

namespace IMMRequest.Domain
{
    public class TypeEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public Topic Topic { get; set; }
        public Guid TopicId { get; set; }
        public Boolean IsDeleted { get; set; }
        public virtual ICollection<AdditionalField> AdditionalFields { get; set; }

        public TypeEntity() 
        {
            this.Id = Guid.NewGuid();
            this.Date = DateTime.Now;
            this.AdditionalFields = new List<AdditionalField>();
            this.IsDeleted = false;
        }

        public TypeEntity(string Name, Topic Topic, ICollection<AdditionalField> AdditionalFields) 
        {
            this.Id = Guid.NewGuid();
            this.Date = DateTime.Now;
            this.Name = Name;
            this.Topic = Topic;
            this.AdditionalFields = AdditionalFields;
            this.IsDeleted = false;
        }
    }
}
