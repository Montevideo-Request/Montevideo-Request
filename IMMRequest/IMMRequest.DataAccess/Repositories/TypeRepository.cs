using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;
using IMMRequest.Exceptions;

namespace IMMRequest.DataAccess
{
    public class TypeRepository : BaseRepository<TypeEntity, Topic>
    {
        private readonly DbSet<TypeEntity> dbSetType;
        public TypeRepository(DbContext context) 
        {
            this.Context = context;
            this.dbSetType = context.Set<TypeEntity>();
        }

        public override TypeEntity Get(Guid id)
        {
            try
            {
                return Context.Set<TypeEntity>()
               .Include(type => type.AdditionalFields)
               .ThenInclude(field => field.Ranges)
               .First(x => x.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new ExceptionController(DataAccessExceptions.NOT_FOUND_TYPE);
            }
        }

        public override IEnumerable<TypeEntity> GetAll()
        {
            return Context.Set<TypeEntity>()
            .Include(type => type.AdditionalFields)
            .ThenInclude(field => field.Ranges)
            .ToList();
        }

        public override Topic GetParent(Guid id)
        {
            try
            {
                return Context.Set<Topic>()
                .Include( topic => topic.Types)
                .First(topic => topic.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new ExceptionController(DataAccessExceptions.NOT_FOUND_PARENT_TOPIC);
            }
        }

        public override bool Exist(TypeEntity type)
        {
            TypeEntity typeToFind = Context.Set<TypeEntity>().Where(a => a.Id == type.Id).FirstOrDefault();
            return !(typeToFind == null);
        }

        public override IEnumerable<TypeEntity> Query(string query)
        {
            throw new NotImplementedException();
        }
    }
}
