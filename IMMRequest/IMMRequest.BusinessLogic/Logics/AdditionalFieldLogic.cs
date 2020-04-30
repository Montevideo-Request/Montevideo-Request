using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.BusinessLogic.Interface;
using IMMRequest.Domain;
using System;
using System.Collections.Generic;

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

        public override void IsValid(AdditionalField additionalField)
        { 
            if(additionalField.Name.Length == 0)
            {
                throw new ExceptionController(ExceptionMessage.INVALID_LENGTH);
            }
            if(additionalField.FieldType.Length == 0)
            {
                throw new ExceptionController(ExceptionMessage.INVALID_LENGTH);
            }
            if(ContainsAdditionalField(additionalField.Name, additionalField.TypeId))
            {
                throw new ExceptionController(ExceptionMessage.ADDITIONAL_FIELD_ALREADY_EXISTS);
            }
        }

        public bool ContainsAdditionalField(string name, Guid typeId)
        {
            bool containsAdditionalFiled = false;
            TypeEntity type = this.repository.GetParent(typeId);
            AdditionalField dummyAdditionalField = new AdditionalField();
            dummyAdditionalField.Name = name;
            if(!type.AdditionalFields.Contains(dummyAdditionalField))
            {
                containsAdditionalFiled = true;
            }
            return containsAdditionalFiled;
        }

        
        public override void EntityExists(Guid id)
        {
            if (!repository.Exist(a => a.Id == id))
            {
                throw new ExceptionController(ExceptionMessage.INVALID_ADDITIONAL_FIELD_ID);
            }
        }

        internal void ContainsRange(Guid additionalFieldId, ICollection<FieldRange> ranges, IRepository<AdditionalField> additionalFieldRepository)
        {
            AdditionalField additionalField = this.repository.Get(additionalFieldId);
            foreach(FieldRange range in ranges)
            {
                if(!additionalField.Ranges.Contains(range))
                {
                    throw new ExceptionController(ExceptionMessage.RANGE_NOT_LISTED);
                }
            }
        }
    }
}
