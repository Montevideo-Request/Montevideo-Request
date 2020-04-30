using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess
{
    public class AdditionalFieldRepository : BaseRepository<AdditionalField, TypeEntity>
    {
        private readonly DbSet<AdditionalField> dbSetAdditionalField;
        public AdditionalFieldRepository (DbContext context) 
        {
            this.Context = context;
            this.dbSetAdditionalField = context.Set<AdditionalField>();
        }
        public override AdditionalField Get(Guid id)
        {
            try
            {
                return Context.Set<AdditionalField>()
               .Include(field => field.Ranges)
               .First(x => x.Id == id);
            }
            catch (InvalidOperationException)
            {

                throw;
            }
        }
        public override IEnumerable<AdditionalField> GetAll()
        {
            return Context.Set<AdditionalField>()
            .Include(field => field.Ranges)
            .ToList();
        }

        public override TypeEntity GetParent(Guid id)
        {
            try
            {
                return Context.Set<TypeEntity>()
                .Include(type => type.AdditionalFields)
                .First(type => type.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }

        public override bool Exist(Func<AdditionalField, bool> predicate)
        {
            return this.dbSetAdditionalField.Where(predicate).Any();
        }

        public override IEnumerable<AdditionalField> Query(string query)
        {
            throw new NotImplementedException();
        }
    }
}
