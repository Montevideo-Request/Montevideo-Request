using System;
using System.Collections.Generic;
using System.Linq;
using IMMRequest.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMMRequest.DataAccess 
{
    public class AdministratorRepository : BaseRepository<Administrator>
    {
        private readonly DbSet<Administrator> dbSetAdministrator;

        public AdministratorRepository (DbContext context) 
        {
            this.Context = context;
            this.dbSetAdministrator = context.Set<Administrator>();
        }

        public override Administrator Get(Guid id) 
        {
            return Context.Set<Administrator>().First(x => x.Id == id);
        }

        public Administrator Get(Func<Administrator, bool> predicate)
        {
            return Context.Set<Administrator>().FirstOrDefault(predicate);
        }

        public override IEnumerable<Administrator> GetAll() 
        {
            return Context.Set<Administrator>().ToList();
        }

        public IEnumerable<Administrator> GetAllByCondition(Func<Administrator, bool> predicate)
        {
            return this.dbSetAdministrator.AsQueryable<Administrator>().Where(predicate);
        }
        
        public override bool Exist(Func<Administrator, bool> predicate)
        {
            return this.dbSetAdministrator.Where(predicate).Any();
        }

        public override IEnumerable<Administrator> Query(string query)
        {
            throw new NotImplementedException();
        }
    }
}