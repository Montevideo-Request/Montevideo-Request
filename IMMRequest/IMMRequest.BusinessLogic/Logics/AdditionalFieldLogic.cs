using System;
using IMMRequest.DataAccess;
using IMMRequest.DataAccess.Interface;
using IMMRequest.Domain;

namespace IMMRequest.BusinessLogic
{
    public class AdditionalFieldLogic : BaseLogic<AdditionalField>
    {
        public IRepository<AdditionalField> additionalFieldRepository;

		public AdditionalFieldLogic(IRepository<AdditionalField> additionalFieldRepository) 
        {
            this.additionalFieldRepository = additionalFieldRepository;
		}

        public AdditionalFieldLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.additionalFieldRepository = new AdditionalFieldRepository(IMMRequestContext);
		}

        public Guid Create(AdditionalField additionalField) 
        {
            try {
                this.additionalFieldRepository.Add(additionalField);
                this.additionalFieldRepository.Save();
                return additionalField.Id;
            } 
            catch {
                throw new ArgumentException("Invalid guid");
            }
            
        }

        public override void Update(AdditionalField entity)
        {
            throw new NotImplementedException();
        }
    }
}