using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;
using IMMRequest.Exceptions;

namespace IMMRequest.DataAccess
{
    public class AreaRepository : BaseRepository<Area, Area>
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
                throw new ExceptionController(DataAccessExceptions.NOT_FOUND_AREA);
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

        public override bool Exist(Area area)
        {
            Area areaToFind = Context.Set<Area>().Where(a => a.Id == area.Id).FirstOrDefault();
            return !(areaToFind == null);
        }
    }
}
