using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;


namespace IMMRequest.DataAccess
{
    public class AreaRepository : BaseRepository<Area, Area>
    {
        public AreaRepository(DbContext context)
        {
            this.Context = context;
        }

        public override bool Exist(Func<Area, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Area> Query(string query)
        {
            throw new NotImplementedException();
        }

        public override Area Get(Guid id)
        {
            try
            {
                return Context.Set<Area>()
               .Include(area => area.Topics)
               .ThenInclude(topic => topic.Types)
               .ThenInclude(type => type.AdditionalFields)
               .ThenInclude(additionalField => additionalField.Ranges)
               .First(x => x.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }

        /* Entity will return itself when it has no parent */
        public override Area GetParent(Guid id)
        {
            return Context.Set<Area>()
            .First(x => x.Id == id);
        }

        public override IEnumerable<Area> GetAll()
        {
            return Context.Set<Area>()
            .Include(area => area.Topics)
            .ThenInclude(topic => topic.Types)
            .ThenInclude(type => type.AdditionalFields)
            .ThenInclude(additionalField => additionalField.Ranges)
            .ToList();
        }
    }
}
