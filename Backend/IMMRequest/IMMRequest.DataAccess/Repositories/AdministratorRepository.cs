using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess
{
    public class AdministratorRepository : IAdministratorRepository<Administrator>
    {
        
        protected DbContext Context { get; set; }
        private readonly DbSet<Administrator> dbSetAdministrator;

        public AdministratorRepository(DbContext context)
        {
            this.Context = context;
            this.dbSetAdministrator = context.Set<Administrator>();
        }

        public void Add(Administrator entity)
        {
            try
            {
                Context.Set<Administrator>().Add(entity);    
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ExceptionController(DataAccessExceptions.GENERIC_ELEMENT_ALREADY_EXISTS);
            }
            
        }

        public void Remove(Administrator entity)
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

        public void Update(Administrator entity)
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

        public Administrator Get(Guid id)
        {
            try
            {
                return Context.Set<Administrator>().First(admin => (admin.Id == id) && !admin.IsDeleted);
            }
            catch (InvalidOperationException)
            {
                throw new ExceptionController(DataAccessExceptions.NOT_FOUND_ADMINISTRATOR);
            }
        }

        public IEnumerable<Administrator> GetAll()
        {
            return Context.Set<Administrator>().ToList().Where(admin => !admin.IsDeleted);
        }

        public IEnumerable<Administrator> GetAllByCondition(Func<Administrator, bool> predicate)
        {
            return this.dbSetAdministrator.AsQueryable<Administrator>().Where(predicate);
        }

        public bool Exist(Administrator administrator)
        {
            Administrator administratorToFind = Context.Set<Administrator>()
            .Where(a => a.Email == administrator.Email && a.Id != administrator.Id && !a.IsDeleted).FirstOrDefault();
            return !(administratorToFind == null);
        }

        public bool Exist(Guid id)
        {
            Administrator administratorToFind = Context.Set<Administrator>().Where(a => (a.Id == id) && !a.IsDeleted).FirstOrDefault();
            return !(administratorToFind == null);
        }

        
        public bool TokenExists(Guid token)
        {
            Administrator administratorToFind = Context.Set<Administrator>().Where(a => a.Token == token).FirstOrDefault();
            return !(administratorToFind == null);
        }

        public Administrator GetByCredentials(Func<Administrator, bool> predicate)
        {
             return Context.Set<Administrator>().FirstOrDefault(predicate);
        }
    }
}
