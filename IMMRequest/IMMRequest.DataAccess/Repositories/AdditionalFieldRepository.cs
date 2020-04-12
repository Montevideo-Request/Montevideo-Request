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
            return Context.Set<AdditionalField>().First(x => x.Id == id);   
        }
        public override IEnumerable<AdditionalField> GetAll() 
        {
            return Context.Set<AdditionalField>().ToList();
        }
    }
}
