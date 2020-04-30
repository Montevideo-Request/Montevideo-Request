using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;


namespace IMMRequest.DataAccess 
{
    public class AreaRepository : BaseRepository<Area>
    {
        private readonly DbSet<Area> dbSetArea;
        public AreaRepository (DbContext context) 
        {
            this.Context = context;
            this.dbSetArea = context.Set<Area>();
        }

        public override bool Exist(Func<Area, bool> predicate)
        {
            return this.dbSetArea.Where(predicate).Any();
        }

        public override IEnumerable<Area> Query(string query)
        {
            throw new NotImplementedException();
        }

        public override Area Get(Guid id) 
        {
            return Context.Set<Area>()
            .Include( area => area.Topics )
            .ThenInclude( topic => topic.Types )
            .ThenInclude( type => type.AdditionalFields )
            .ThenInclude( additionalField => additionalField.Ranges )
            .First(x => x.Id == id);
        }

        public override IEnumerable<Area> GetAll() 
        {
            return Context.Set<Area>()
            .Include( area => area.Topics )
            .ThenInclude( topic => topic.Types )
            .ThenInclude( type => type.AdditionalFields )
            .ThenInclude( additionalField => additionalField.Ranges )
            .ToList();
        }
    }
}
