using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;


namespace IMMRequest.DataAccess 
{
    public class AreaRepository : BaseRepository<Area>
    {
        public AreaRepository (DbContext context) 
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
            return Context.Set<Area>().First(x => x.Id == id);
        }

        public override IEnumerable<Area> GetAll() 
        {
            return Context.Set<Area>().ToList();
        }
    }
}
