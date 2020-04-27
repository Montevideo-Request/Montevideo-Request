using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess 
{
    public class AdditionalFieldRepository : BaseRepository<AdditionalField>
    {
        public AdditionalFieldRepository (DbContext context) 
        {
            this.Context = context;
        }
        public override AdditionalField Get(Guid id) 
        {
            return Context.Set<AdditionalField>()
            .Include( field => field.Ranges )
            .First(x => x.Id == id);   
        }
        public override IEnumerable<AdditionalField> GetAll() 
        {
            return Context.Set<AdditionalField>()
            .Include( field => field.Ranges )
            .ToList();
        }

        public override bool Exist(Func<AdditionalField, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<AdditionalField> Query(string query)
        {
            throw new NotImplementedException();
        }
    }
}
