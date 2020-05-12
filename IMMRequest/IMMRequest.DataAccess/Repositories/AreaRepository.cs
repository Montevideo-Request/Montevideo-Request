using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.DataAccess.Interface;
using IMMRequest.Domain;
using System.Linq;
using System;
using IMMRequest.Exceptions;

namespace IMMRequest.DataAccess
{
    public class AreaRepository : IAreaRepository<Area>
    {
        private readonly DbSet<Area> dbSetArea;
        protected DbContext Context { get; set; }
        public AreaRepository(DbContext context)
        {
            this.Context = context;
            this.dbSetArea = context.Set<Area>();
        }

        public void Add(Area entity)
        {
            try
            {
                Context.Set<Area>().Add(entity);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ExceptionController(DataAccessExceptions.GENERIC_ELEMENT_ALREADY_EXISTS);
            }

        }

        public void Remove(Area entity)
        {
            try
            {
                Context.Set<Area>().Remove(entity);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ExceptionController(DataAccessExceptions.GENERIC_ELEMENT_ALREADY_EXISTS);
            }
        }

        public void Update(Area entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ExceptionController(DataAccessExceptions.GENERIC_ELEMENT_ALREADY_EXISTS);
            }
        }

        public void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ExceptionController(DataAccessExceptions.GENERIC_ELEMENT_ALREADY_EXISTS);
            }
        }

        public Area Get(Guid id)
        {
            try
            {
                return Context.Set<Area>()
               .Include(area => area.Topics)
               .ThenInclude(topic => topic.Types)
               .ThenInclude(type => type.AdditionalFields)
               .ThenInclude(additionalField => additionalField.Ranges)
               .First(x => x.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new ExceptionController(DataAccessExceptions.NOT_FOUND_AREA);
            }
        }

        public IEnumerable<Area> GetAll()
        {
            return Context.Set<Area>()
            .Include(area => area.Topics)
            .ThenInclude(topic => topic.Types)
            .ThenInclude(type => type.AdditionalFields)
            .ThenInclude(additionalField => additionalField.Ranges)
            .ToList();
        }

        public bool Exist(Area area)
        {
            Area areaToFind = Context.Set<Area>().Where(a => a.Name == area.Name || a.Id == area.Id).FirstOrDefault();
            return !(areaToFind == null);
        }
        public bool NameExists(Area area)
        {
            Area areaToFind = Context.Set<Area>().Where(a => a.Name == area.Name).FirstOrDefault();
            return !(areaToFind == null);
        }
    }
}
