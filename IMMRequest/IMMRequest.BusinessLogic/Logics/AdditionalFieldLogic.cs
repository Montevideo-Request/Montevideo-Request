using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
using System;
using System.Collections.Generic;
using IMMRequest.BusinessLogic.Interface;

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
            if(additionalField.Name != null && additionalField.Name.Length == 0)
            {
                throw new ExceptionController(LogicExceptions.INVALID_LENGTH);
            }
            if(additionalField.FieldType != null && additionalField.FieldType.Length == 0)
            {
                throw new ExceptionController(LogicExceptions.INVALID_LENGTH);
            }
            if(ContainsAdditionalField(additionalField.Name, additionalField.TypeId))
            {
                throw new ExceptionController(LogicExceptions.ALREADY_EXISTS_ADDITIONAL_FIELD);
            }
            
            FieldRangeLogic selectedStrategy = new FieldRangeLogic(additionalField.FieldType);
            selectedStrategy.ValidateRanges(additionalField.FieldType, additionalField.Ranges);
        }

        public bool ContainsAdditionalField(string name, Guid typeId)
        {
            bool containsAdditionalFiled = false;
            TypeEntity type = this.repository.GetParent(typeId);
            AdditionalField dummyAdditionalField = new AdditionalField();
            dummyAdditionalField.Name = name;
            if(type.AdditionalFields.Contains(dummyAdditionalField))
            {
                containsAdditionalFiled = true;
            }
            return containsAdditionalFiled;
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

        public override void EntityExist(AdditionalField entity)
        {
            if(this.repository.Exist(entity))
            {
                throw new ExceptionController(LogicExceptions.ALREADY_EXISTS_ADDITIONAL_FIELD);
            }
        }
        
        public override void NotExist(Guid id)
        {
            AdditionalField dummyAdditionalField = new AdditionalField();
            dummyAdditionalField.Id = id;
            if(!this.repository.Exist(dummyAdditionalField)){
                throw new ExceptionController(LogicExceptions.INVALID_ID_ADDITIONAL_FIELD);
            }
        }
    }
}
