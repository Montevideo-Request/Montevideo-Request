using System;
using System.Collections.Generic;
using System.Linq;
using IMMRequest.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMMRequest.DataAccess 
{
    public class AdministratorRepository 
    {
        
        protected DbContext Context { get; set; }

        public AdministratorRepository (DbContext context) 
        {
            this.Context = context;
        }

        public Administrator Get(Guid id) 
        {
            return Context.Set<Administrator>().First(x => x.Id == id);
        }

        public IEnumerable<Administrator> GetAll() 
        {
            return Context.Set<Administrator>().ToList();
        }

        public void Add(Administrator entity) 
        {
            Context.Set<Administrator>().Add(entity);
        }

        public void Remove(Administrator entity) 
        {
            Context.Set<Administrator>().Remove(entity);
        }

        public void Update(Administrator entity) 
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Save() 
        {
            Context.SaveChanges();
        }
    }
}