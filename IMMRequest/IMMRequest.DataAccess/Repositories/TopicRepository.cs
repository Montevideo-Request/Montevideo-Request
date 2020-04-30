using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess 
{
    public class TopicRepository : BaseRepository<Topic>
    {
        private readonly DbSet<Topic> dbSetTopic;
        public TopicRepository (DbContext context) 
        {
            this.Context = context;
            this.dbSetTopic = context.Set<Topic>();
        }

        public override Topic Get(Guid id) 
        {
            return Context.Set<Topic>()
            .Include( topic => topic.Types )
            .ThenInclude( type => type.AdditionalFields )
            .ThenInclude( additionalField => additionalField.Ranges )
            .First(topic => topic.Id == id);
        }

        public override IEnumerable<Topic> GetAll() 
        {
            return Context.Set<Topic>()
            .Include( topic => topic.Types )
            .ThenInclude( type => type.AdditionalFields )
            .ThenInclude( additionalField => additionalField.Ranges )
            .ToList();
        }

        public override bool Exist(Func<Topic, bool> predicate)
        {
            return this.dbSetTopic.Where(predicate).Any();
        }

        public override IEnumerable<Topic> Query(string query)
        {
            throw new NotImplementedException();
        }
    }
}
