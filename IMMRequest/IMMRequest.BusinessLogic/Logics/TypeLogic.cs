using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using IMMRequest.BusinessLogic.Interface;
using System;
using System.Collections.Generic;

namespace IMMRequest.BusinessLogic
{
    public class TypeLogic : BaseLogic<TypeEntity, Topic>
    {
        public TypeLogic(IRepository<TypeEntity, Topic> typeRepository) 
        {
            this.repository = typeRepository;
        }

        public TypeLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.repository = new TypeRepository(IMMRequestContext);
		}

        public override void Update(TypeEntity entity)
        {
            try
            {
                TypeEntity typeEntityToUpdate = this.repository.Get(entity.Id);
                typeEntityToUpdate.Name = entity.Name;
                this.repository.Update(typeEntityToUpdate);
                this.repository.Save();
            }
            catch
            {
                throw new ExceptionController(ExceptionMessage.NOT_IMPLEMENTED);
            } 
        }

        public override void IsValid(TypeEntity type)
        { 
            if(type.Name.Length == 0)
            {
                throw new ExceptionController(ExceptionMessage.INVALID_LENGTH);
            }
            if(ContainsType(type.Name, type.TopicId))
            {
                throw new ExceptionController(ExceptionMessage.TYPE_ALREADY_EXISTS);
            }
        }

        public bool ContainsType(string name, Guid topicId)
        {
            bool containsType = false;
            Topic topic = this.repository.GetParent(topicId);
            TypeEntity dummyTypeEntity = new TypeEntity();
            dummyTypeEntity.Name = name;
            if(!topic.Types.Contains(dummyTypeEntity))
            {
                containsType = true;
            }
            return containsType;
        }

        public override void EntityExists(Guid id)
        {
            if (!this.repository.Exist(a => a.Id == id))
            {
                throw new ExceptionController(ExceptionMessage.INVALID_TYPE_ID);
            }
        }
        
        public void EntityExistsIn(AdditionalField additionalField, Guid TypeId)
        {
            EntityExists(TypeId);
            TypeEntity typeEntity = this.repository.Get(TypeId);
            if(!typeEntity.AdditionalFields.Contains(additionalField))
            {
                throw new ExceptionController(ExceptionMessage.INVALID_ADDITIONAL_FIELD);
            }
        }
    }
}
