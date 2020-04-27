using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess 
{
    public class TopicRepository : BaseRepository<Topic>
    {
        public TopicRepository (DbContext context) 
        {
            this.Context = context;
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
            throw new NotImplementedException();
        }

        public override IEnumerable<Topic> Query(string query)
        {
            throw new NotImplementedException();
        }
    }
}
