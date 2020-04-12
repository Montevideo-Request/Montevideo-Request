using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System.Linq;
using System;

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

        public void Add(TypeEntity type)
        {
            this.typeRepository.Add(type);
        }

        public void Save()
        {
            this.typeRepository.Save();
        }        

        public TypeEntity Create(TypeEntity type) 
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
                TypeEntity type = this.typeRepository.Get(id);
                this.typeRepository.Remove(type);
                this.typeRepository.Save();
            }
            catch
            {
                throw new ArgumentException("Invalid Id");
            }
        }

		public TypeEntity Get(Guid id) 
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

        public IEnumerable<TypeEntity> GetTypes() 
        {
            IEnumerable<TypeEntity> types = this.typeRepository.GetAll();
            
            if (types.Count() == 0) 
            {
                throw new ArgumentException("There are no Requests");
            }

            return types;
		}
    }
}
