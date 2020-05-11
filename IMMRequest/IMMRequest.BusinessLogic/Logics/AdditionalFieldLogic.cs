using IMMRequest.BusinessLogic.Interface;
using IMMRequest.DataAccess.Interface;
using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic
{
    public class AdditionalFieldLogic : IAdditionalFieldLogic<AdditionalField, FieldRange>
    {
        protected IRepository<AdditionalField, TypeEntity> repository { get; set; }

        public AdditionalFieldLogic(IRepository<AdditionalField, TypeEntity> additionalFieldRepository)
        {
            this.repository = additionalFieldRepository;
        }

        public AdditionalFieldLogic()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            this.repository = new AdditionalFieldRepository(IMMRequestContext);
        }

        public AdditionalField Update(AdditionalField entity)
        {
            NotExist(entity.Id);
            AdditionalField additionalFieldToUpdate = this.repository.Get(entity.Id);
            additionalFieldToUpdate.Name = entity.Name != null ? entity.Name : additionalFieldToUpdate.Name;
            additionalFieldToUpdate.FieldType = entity.FieldType != null ? entity.FieldType : additionalFieldToUpdate.FieldType;
            this.repository.Update(additionalFieldToUpdate);
            this.repository.Save();

            return additionalFieldToUpdate;
        }

        public void IsValid(AdditionalField additionalField)
        {
            if ( (additionalField.Name != null && additionalField.Name.Length == 0) || additionalField.Name == null)
            {
                throw new ExceptionController(LogicExceptions.INVALID_LENGTH);
            }
            if ( (additionalField.FieldType != null && additionalField.FieldType.Length == 0) || additionalField.FieldType == null)
            {
                throw new ExceptionController(LogicExceptions.INVALID_LENGTH);
            }
            if (ContainsAdditionalField(additionalField))
            {
                throw new ExceptionController(LogicExceptions.ALREADY_EXISTS_ADDITIONAL_FIELD);
            }

            ValidRanges(additionalField);
            FieldRangeLogic selectedStrategy = new FieldRangeLogic(additionalField.FieldType);
            selectedStrategy.ValidRangeFormat(additionalField);
        }

        public void ValidRanges(AdditionalField additionalField)
        {
            RepeatedRanges(additionalField.Ranges);
            foreach (FieldRange range in additionalField.Ranges)
            {
                if (range.AdditionalFieldId != additionalField.Id)
                {
                    throw new ExceptionController(LogicExceptions.INVALID_ID_ADDITIONAL_FIELD_IN_RANGE);
                }
            }
        }
        public void RepeatedRanges(ICollection<FieldRange> ranges)
        {
            if (ranges.Count() > 0)
            {
                var rangeObj = ranges.GroupBy(x => x.Range);

                foreach (var repetition in rangeObj)
                {
                    if (repetition.Count() > 1)
                    {
                        throw new ExceptionController(LogicExceptions.RANGE_REPEATED_IN_LIST);    
                    }
                }
            }
        }

        public bool ContainsAdditionalField(AdditionalField additionalField)
        {
            bool containsAdditionalFiled = false;
            TypeEntity type = this.repository.GetParent(additionalField.TypeId);
            AdditionalField dummyAdditionalField = new AdditionalField();

            dummyAdditionalField.Name = additionalField.Name;
            if (type.AdditionalFields.Contains(dummyAdditionalField))
            {
                containsAdditionalFiled = true;
            }
            return containsAdditionalFiled;
        }

        internal void ContainsRange(Guid additionalFieldId, ICollection<FieldRange> ranges, IRepository<AdditionalField, TypeEntity> additionalFieldRepository)
        {
            AdditionalField additionalField = this.repository.Get(additionalFieldId);
            foreach (FieldRange range in ranges)
            {
                if (!additionalField.Ranges.Contains(range))
                {
                    throw new ExceptionController(LogicExceptions.RANGE_NOT_LISTED);
                }
            }
        }

        public void EntityExist(AdditionalField entity)
        {
            if (this.repository.Exist(entity))
            {
                throw new ExceptionController(LogicExceptions.ALREADY_EXISTS_ADDITIONAL_FIELD);
            }
        }

        public void NotExist(Guid id)
        {
            AdditionalField dummyAdditionalField = new AdditionalField();
            dummyAdditionalField.Id = id;
            if (!this.repository.Exist(dummyAdditionalField))
            {
                throw new ExceptionController(LogicExceptions.INVALID_ID_ADDITIONAL_FIELD);
            }
        }

        public FieldRange AddFieldRange(Guid additionalFieldId, FieldRange field)
        {
            if (field.Range == null || field.Range.Length == 0)
            {
                throw new ExceptionController(LogicExceptions.EMPTY_RANGE_INPUT);
            }
            AdditionalField additionalField = this.repository.Get(additionalFieldId);
            if (additionalField.Ranges.Contains(field))
            {
                throw new ExceptionController(LogicExceptions.RANGE_NOT_LISTED);
            }
            additionalField.Ranges.Add(field);
            this.repository.Update(additionalField);
            this.repository.Save();
            return field;
        }

        public void Save()
        {
            this.repository.Save();
        }

        public AdditionalField Create(AdditionalField entity)
        {
            IsValid(entity);
            this.repository.Add(entity);
            this.repository.Save();
            return entity;
        }
        public void Remove(Guid id)
        {
            NotExist(id);
            AdditionalField additionalFieldToDelete = this.repository.Get(id);
            this.repository.Remove(additionalFieldToDelete);
            this.repository.Save();
        }

        public AdditionalField Get(Guid id)
        {
            NotExist(id);
            return this.repository.Get(id);
        }

        public IEnumerable<AdditionalField> GetAll()
        {
            IEnumerable<AdditionalField> entities = this.repository.GetAll();

            if (entities.ToList().Count() == 0)
            {
                throw new ExceptionController(LogicExceptions.GENERIC_NO_ELEMENTS);
            }

            return entities;
        }

        public IEnumerable<FieldRange> GetAllRanges(Guid id)
        {
            AdditionalField field = this.repository.Get(id);
            IEnumerable<FieldRange> entities = field.Ranges;

            if (entities.ToList().Count() == 0)
            {
                throw new ExceptionController(LogicExceptions.GENERIC_NO_ELEMENTS);
            }

            return entities;
        }
    }
}
