using Microsoft.EntityFrameworkCore;
using IMMRequest.DataAccess.Interface;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;
using IMMRequest.Exceptions;

namespace IMMRequest.DataAccess
{
    public class RequestRepository : IRequestRepository<Request, TypeEntity>
    {
        protected DbContext Context { get; set; }
        public RequestRepository(DbContext context)
        {
            this.Context = context;
        }

         public void Add(Request entity)
        {
            try
            {
                Context.Set<Request>().Add(entity);    
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ExceptionController(DataAccessExceptions.GENERIC_ELEMENT_ALREADY_EXISTS);
            }
            
        }

        public Request Get(Guid id)
        {
            try
            {
                return Context.Set<Request>().Include(values => values.AdditionalFieldValues).First(x => x.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new ExceptionController(DataAccessExceptions.NOT_FOUND_REQUEST);
            }
        }
        public IEnumerable<Request> GetAll()
        {
            return Context.Set<Request>()
            .Include(values => values.AdditionalFieldValues)
            .ToList();
        }

        public TypeEntity GetTypeWithFields(Guid id)
        {
            try
            {
                return Context.Set<TypeEntity>()
                .Include( type => type.AdditionalFields)
                .ThenInclude( fields => fields.Ranges )
                .First(x => x.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new ExceptionController(DataAccessExceptions.NOT_FOUND_TYPE);
            }
        }

        public bool Exist(Request request)
        {
            Request requestToFind = Context.Set<Request>().Where(a => a.Id == request.Id).FirstOrDefault();
            return !(requestToFind == null);
        }
        
        public bool Exist(Func<Request, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(Request entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Modified;    
            }
            catch (DbUpdateConcurrencyException)
            {
                /* TODO Exception Request already exists. */
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
    }
}
