using System;
using System.Collections.Generic;
using System.Linq;
using IMMRequest.Domain;
using IMMRequest.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace IMMRequest.DataAccess
{
    public class AdministratorRepository : BaseRepository<Administrator, Administrator>
    {
        private readonly DbSet<Administrator> dbSetAdministrator;

        public AdministratorRepository(DbContext context)
        {
            this.Context = context;
            this.dbSetAdministrator = context.Set<Administrator>();
        }

        public override Administrator Get(Guid id)
        {
            try
            {
                return Context.Set<Administrator>().First(admin => admin.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new ExceptionController(DataAccessExceptions.NOT_FOUND_ADMINISTRATOR);
            }
        }

        /* Entity will return itself when it has no parent */
        public override Administrator GetParent(Guid id)
        {
            try
            {
                return Context.Set<Administrator>().First(admin => admin.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new ExceptionController(DataAccessExceptions.NOT_FOUND_PARENT_ADMINISTRATOR);
            }
        }

        public override IEnumerable<Administrator> GetAll()
        {
            return Context.Set<Administrator>().ToList();
        }

        public IEnumerable<Administrator> GetAllByCondition(Func<Administrator, bool> predicate)
        {
            return this.dbSetAdministrator.AsQueryable<Administrator>().Where(predicate);
        }

        public Administrator Get(Func<Administrator, bool> predicate)
        {
            return Context.Set<Administrator>().FirstOrDefault(predicate);
        }

        public override IEnumerable<Administrator> Query(string query)
        {
            throw new NotImplementedException();
        }

        public override bool Exist(Administrator administrator)
        {
            Administrator administratorToFind = Context.Set<Administrator>().Where(a => a.Id == administrator.Id || a.Token == administrator.Token).FirstOrDefault();
            return !(administratorToFind == null);
        }
    }
}
