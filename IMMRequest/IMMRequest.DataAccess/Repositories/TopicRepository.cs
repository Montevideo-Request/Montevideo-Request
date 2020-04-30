using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess
{
    public class TopicRepository : BaseRepository<Topic, Area>
    {
        public TopicRepository(DbContext context)
        {
            this.Context = context;
        }

        public override Topic Get(Guid id)
        {
            try
            {
                return Context.Set<Topic>()
               .Include(topic => topic.Types)
               .ThenInclude(type => type.AdditionalFields)
               .ThenInclude(additionalField => additionalField.Ranges)
               .First(topic => topic.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }

        public override IEnumerable<Topic> GetAll()
        {
            return Context.Set<Topic>()
            .Include(topic => topic.Types)
            .ThenInclude(type => type.AdditionalFields)
            .ThenInclude(additionalField => additionalField.Ranges)
            .ToList();
        }
        public override Area GetParent(Guid id)
        {
            return Context.Set<Area>()
            .Include( area => area.Topics)
            .First(area => area.Id == id);
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
