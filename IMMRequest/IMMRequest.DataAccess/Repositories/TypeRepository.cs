using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess 
{
    public class TypeRepository : BaseRepository<TypeEntity>
    {
        public TypeRepository (DbContext context) 
        {
            this.Context = context;
        }

        public override TypeEntity Get(Guid id) 
        {
            return Context.Set<TypeEntity>().Include( x => x.AdditionalFields ).First(x => x.Id == id);
            
        }

        public override IEnumerable<TypeEntity> GetAll() 
        {
            return Context.Set<TypeEntity>().Include( x => x.AdditionalFields ).ToList();
        }

        public override bool Exist(Func<TypeEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TypeEntity> Query(string query)
        {
            throw new NotImplementedException();
        }
    }
}
