using System;
using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System.Linq;

namespace IMMRequest.BusinessLogic
{
    public class TypeLogic
    {
        public TypeRepository typeRepository;

        public TypeLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.typeRepository = new TypeRepository(IMMRequestContext);
		}

        public void Add(IMMRequest.Domain.Type type)
        {
            this.typeRepository.Add(type);
        }

        public void Save()
        {
            this.typeRepository.Save();
        }        

        public IMMRequest.Domain.Type Create(IMMRequest.Domain.Type type) 
        {
            try
            {
                this.Add(type);
                return type;
            } 
            catch 
            {
                throw new ArgumentException("Id already exists");
            }
        }

        public void Remove(Guid id) 
        {
            try 
            {
                IMMRequest.Domain.Type type = this.typeRepository.Get(id);
                this.typeRepository.Remove(type);
                this.typeRepository.Save();
            }
            catch
            {
                throw new ArgumentException("Invalid Id");
            }
        }

		public IMMRequest.Domain.Type Get(Guid id) 
        {
            try
            {
                return this.typeRepository.Get(id);
            }
            catch 
            {
                throw new ArgumentException("Invalid Id");
            }
        }

        public IEnumerable<IMMRequest.Domain.Type> GetTypes() 
        {
            IEnumerable<IMMRequest.Domain.Type> types = this.typeRepository.GetAll();
            
            if (types.Count() == 0) 
            {
                throw new ArgumentException("There are no Requests");
            }

            return types;
		}
    }
}