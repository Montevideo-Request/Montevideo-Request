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
            return Context.Set<TypeEntity>().First(x => x.Id == id);
            
        }

        public override IEnumerable<TypeEntity> GetAll() 
        {
            return Context.Set<TypeEntity>().ToList();
        }
    }
}
