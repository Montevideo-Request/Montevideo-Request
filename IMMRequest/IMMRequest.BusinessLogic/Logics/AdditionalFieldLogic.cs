using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.Exceptions;
using IMMRequest.Domain;

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
            try
            {
                AdditionalField additionalFieldToUpdate = this.repository.Get(entity.Id);
                additionalFieldToUpdate.Name = entity.Name;
                additionalFieldToUpdate.FieldType = entity.FieldType;
                this.repository.Update(additionalFieldToUpdate);
                this.repository.Save();
            }
            catch
            {
                throw new ExceptionController(ExceptionMessage.INVALID_ID);
            }   
        }
    }
}
