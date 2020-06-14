using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;
using IMMRequest.Exceptions;

namespace IMMRequest.DataAccess
{
    public class TopicRepository : BaseRepository<Topic, Area>
    {
        private readonly DbSet<Topic> dbSetTopic;
        public TopicRepository(DbContext context) 
        {
            this.Context = context;
            this.dbSetTopic = context.Set<Topic>();
        }

        public override void Remove(Topic entity)
        {
            try
            {
                entity.IsDeleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ExceptionController(DataAccessExceptions.GENERIC_ELEMENT_ALREADY_EXISTS);
            }
        }

        public override Topic Get(Guid id)
        {
            try
            {
                return Context.Set<Topic>()
               .Include(topic => topic.Types)
               .ThenInclude(type => type.AdditionalFields)
               .ThenInclude(additionalField => additionalField.Ranges)
               .Where( x => !x.IsDeleted )
               .First(topic => topic.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new ExceptionController(DataAccessExceptions.NOT_FOUND_TOPIC);
            }
        }

        public override IEnumerable<Topic> GetAll()
        {
            return Context.Set<Topic>()
            .Include(topic => topic.Types)
            .ThenInclude(type => type.AdditionalFields)
            .ThenInclude(additionalField => additionalField.Ranges)
            .Where( x => !x.IsDeleted )
            .ToList();
        }
        public override Area GetParent(Guid id)
        {
            return Context.Set<Area>()
            .Include( area => area.Topics)
            .First(area => (area.Id == id) && !area.IsDeleted);
        }

        public override bool Exist(Topic topic)
        {
            Topic topicToFind = Context.Set<Topic>().Where(a => (a.Id == topic.Id) && !a.IsDeleted).FirstOrDefault();
            return !(topicToFind == null);
        }

        public override bool NameExists(Topic topic)
        {
            Topic topicToFind = Context.Set<Topic>().Where(t => (t.Name == topic.Name) && (t.AreaId == topic.AreaId) && !t.IsDeleted).FirstOrDefault();
            return !(topicToFind == null);
        }
    }
}
