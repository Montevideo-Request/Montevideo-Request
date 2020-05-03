using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Domain;
using IMMRequest.Exceptions;
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

                throw new ExceptionController(DataAccessExceptions.NOT_FOUND_ADDITIONAL_FIELD);
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
                throw new ExceptionController(DataAccessExceptions.NOT_FOUND_PARENT_TYPE);
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

        public override bool Exist(AdditionalField additionalField)
        {
            AdditionalField additionalFieldToFind = Context.Set<AdditionalField>().Where(a => a.Id == additionalField.Id).FirstOrDefault();
            return !(additionalFieldToFind == null);
        }
    }
}
