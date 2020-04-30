using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
using System;
using System.Collections.Generic;

namespace IMMRequest.BusinessLogic
{
    public class AdditionalFieldLogic : BaseLogic<AdditionalField, TypeEntity>
    {
		public AdditionalFieldLogic(IRepository<AdditionalField, TypeEntity> additionalFieldRepository) 
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
                throw new ExceptionController(LogicExceptions.INVALID_ID_ADDITIONAL_FIELD);
            }   
        }

        public override void IsValid(AdditionalField additionalField)
        { 
            if(additionalField.Name.Length == 0)
            {
                throw new ExceptionController(LogicExceptions.INVALID_LENGTH);
            }
            if(additionalField.FieldType.Length == 0)
            {
                throw new ExceptionController(LogicExceptions.INVALID_LENGTH);
            }
            if(ContainsAdditionalField(additionalField.Name, additionalField.TypeId))
            {
                throw new ExceptionController(LogicExceptions.ALREADY_EXISTS_ADDITIONAL_FIELD);
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
                throw new ExceptionController(LogicExceptions.INVALID_ID_ADDITIONAL_FIELD);
            }
        }

        internal void ContainsRange(Guid additionalFieldId, ICollection<FieldRange> ranges, IRepository<AdditionalField, TypeEntity> additionalFieldRepository)
        {
            AdditionalField additionalField = this.repository.Get(additionalFieldId);
            foreach(FieldRange range in ranges)
            {
                if(!additionalField.Ranges.Contains(range))
                {
                    throw new ExceptionController(LogicExceptions.RANGE_NOT_LISTED);
                }
            }
        }
    }
}
