using System;
using System.Collections.Generic;
using System.Linq;
using IMMRequest.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMMRequest.DataAccess 
{
    public class AdministratorRepository : BaseRepository<Administrator>
    {
        public AdministratorRepository (DbContext context) 
        {
            this.Context = context;
        }

        public override Administrator Get(Guid id) 
        {
            return Context.Set<Administrator>().First(x => x.Id == id);
        }

        public override IEnumerable<Administrator> GetAll() 
        {
            return Context.Set<Administrator>().ToList();
        }
    }
}