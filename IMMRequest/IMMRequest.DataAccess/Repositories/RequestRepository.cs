using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess 
{
    public class RequestRepository : BaseRepository<Request>
    {
        public RequestRepository (DbContext context) 
        {
            this.Context = context;
        }
        public override Request Get(Guid id) 
        {
            return Context.Set<Request>()
            .Include( values => values.FieldRangeValues)
            .First(x => x.Id == id);
            
        }
        public override IEnumerable<Request> GetAll() 
        {
            return Context.Set<Request>()
            .Include( values => values.FieldRangeValues)
            .ToList();
        }

        public override bool Exist(Func<Request, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Request> Query(string query)
        {
            throw new NotImplementedException();
        }
    }
}
