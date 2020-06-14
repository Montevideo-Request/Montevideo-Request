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

        public override void Remove(AdditionalField entity)
        {
            try
            {
                entity.IsDeleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ExceptionController(DataAccessExceptions.GENERIC_ELEMENT_ALREADY_EXISTS);
            }
        }
        public override AdditionalField Get(Guid id)
        {
            try
            {
                return Context.Set<AdditionalField>()
               .Include(field => field.Ranges)
               .First(x => (x.Id == id) && !x.IsDeleted);
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
            .Where( x => !x.IsDeleted)
            .ToList();
        }

        public override TypeEntity GetParent(Guid id)
        {
            try
            {
                return Context.Set<TypeEntity>()
                .Include(type => type.AdditionalFields)
                .First(type => (type.Id == id) && !type.IsDeleted);
            }
            catch (InvalidOperationException)
            {
                throw new ExceptionController(DataAccessExceptions.NOT_FOUND_PARENT_TYPE);
            }
        }

        public override bool Exist(AdditionalField additionalField)
        {
            AdditionalField additionalFieldToFind = Context.Set<AdditionalField>()
            .Where(a => (a.Id == additionalField.Id) && !a.IsDeleted).FirstOrDefault();
            return !(additionalFieldToFind == null);
        }

        public override bool NameExists(AdditionalField field)
        {
            AdditionalField fieldToFind = Context.Set<AdditionalField>()
            .Where(t => (t.Name == field.Name) && (t.TypeId == field.TypeId) && !t.IsDeleted).FirstOrDefault();
            return !(fieldToFind == null);
        }
    }
}
