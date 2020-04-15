using System;
using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System.Linq;
using IMMRequest.BusinessLogic.Interface;

namespace IMMRequest.BusinessLogic
{
    public class AdditionalFieldLogic : ILogic<AdditionalField>
    {
        public AdditionalFieldRepository additionalFieldRepository;

        public AdditionalFieldLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.additionalFieldRepository = new AdditionalFieldRepository(IMMRequestContext);
		}

        public void Add(AdditionalField additionalField)
        {
            this.additionalFieldRepository.Add(additionalField);
        }

        public void Save()
        {
            this.additionalFieldRepository.Save();
        }        

        public AdditionalField Create(AdditionalField additionalField) 
        {
            try
            {
                this.Add(additionalField);
                return additionalField;
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
                AdditionalField additionalField = this.additionalFieldRepository.Get(id);
                this.additionalFieldRepository.Remove(additionalField);
                this.additionalFieldRepository.Save();
            }
            catch
            {
                throw new ArgumentException("Invalid Id");
            }
        }

		public AdditionalField Get(Guid id) 
        {
            try
            {
                return this.additionalFieldRepository.Get(id);
            }
            catch 
            {
                throw new ArgumentException("Invalid Id");
            }
        }

        public IEnumerable<AdditionalField> GetAll() 
        {
            IEnumerable<AdditionalField> additionalFields = this.additionalFieldRepository.GetAll();
            
            if (additionalFields.Count() == 0) 
            {
                throw new ArgumentException("There are no Requests");
            }

            return additionalFields;
		}

        public void Update(AdditionalField entity)
        {
            throw new NotImplementedException();
        }
    }
}