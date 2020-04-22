using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System;

namespace IMMRequest.BusinessLogic
{
    public class AdditionalFieldLogic : BaseLogic<AdditionalField>
    {
		public AdditionalFieldLogic(IRepository<AdditionalField> additionalFieldRepository) 
        {
            this.repository = additionalFieldRepository;
		}

        public AdditionalFieldLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.repository = new AdditionalFieldRepository(IMMRequestContext);
		}

        public override void Update(AdditionalField entity)
        {
            throw new NotImplementedException();
        }
    }
}
