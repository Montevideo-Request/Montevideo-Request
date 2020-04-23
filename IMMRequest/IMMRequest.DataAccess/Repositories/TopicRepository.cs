using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess 
{
    public class TopicRepository : BaseRepository<Topic>
    {
        public TopicRepository (DbContext context) 
        {
            this.Context = context;
        }

        public override Topic Get(Guid id) 
        {
            return Context.Set<Topic>().First(x => x.Id == id);
            
        }

        public override IEnumerable<Topic> GetAll() 
        {
            return Context.Set<Topic>().ToList();
        }

        public override bool Exist(Func<Topic, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Topic> Query(string query)
        {
            throw new NotImplementedException();
        }
    }
}
