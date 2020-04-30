using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess 
{
    public class TypeRepository : BaseRepository<TypeEntity>
    {
        private readonly DbSet<TypeEntity> dbSetType;
        public TypeRepository (DbContext context) 
        {
            this.Context = context;
            this.dbSetType = context.Set<TypeEntity>();
        }

        public override TypeEntity Get(Guid id) 
        {
            return Context.Set<TypeEntity>()
            .Include( type => type.AdditionalFields )
            .ThenInclude( field => field.Ranges) 
            .First(x => x.Id == id);
        }

        public override IEnumerable<TypeEntity> GetAll() 
        {
            return Context.Set<TypeEntity>()
            .Include( type => type.AdditionalFields )
            .ThenInclude( field => field.Ranges) 
            .ToList();
        }

        public override bool Exist(Func<TypeEntity, bool> predicate)
        {
            return this.dbSetType.Where(predicate).Any();
        }

        public override IEnumerable<TypeEntity> Query(string query)
        {
            throw new NotImplementedException();
        }
    }
}
