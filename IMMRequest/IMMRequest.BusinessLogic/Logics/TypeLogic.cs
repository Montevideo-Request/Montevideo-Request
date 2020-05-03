using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using IMMRequest.Exceptions;
using System;

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
                throw new ExceptionController(LogicExceptions.NOT_IMPLEMENTED);
            } 
        }

        public override void IsValid(TypeEntity type)
        { 
            if(type.Name != null && type.Name.Length == 0)
            {
                throw new ExceptionController(LogicExceptions.INVALID_LENGTH);
            }
            if(ContainsType(type.Name, type.TopicId))
            {
                throw new ExceptionController(LogicExceptions.ALREADY_EXISTS_TYPE);
            }
        }

        public bool ContainsType(string name, Guid topicId)
        {
            bool containsType = false;
            Topic topic = this.repository.GetParent(topicId);
            TypeEntity dummyTypeEntity = new TypeEntity();
            dummyTypeEntity.Name = name;
            if(topic.Types.Contains(dummyTypeEntity))
            {
                containsType = true;
            }
            return containsType;
        }

        public void EntityExistsIn(AdditionalField additionalField, Guid TypeId)
        {
            
            TypeEntity dummyType = new TypeEntity();
            dummyType.Id = TypeId;
            EntityExist(dummyType);
            TypeEntity typeEntity = this.repository.Get(TypeId);
            if(!typeEntity.AdditionalFields.Contains(additionalField))
            {
                throw new ExceptionController(LogicExceptions.INVALID_ADDITIONAL_FIELD);
            }
        }

        public override void EntityExist(TypeEntity entity)
        {
            if(this.repository.Exist(entity))
            {
                throw new ExceptionController(LogicExceptions.ALREADY_EXISTS_TYPE);
            }
        }

        public override void NotExist(Guid id)
        {
            TypeEntity dummyType = new TypeEntity();
            dummyType.Id = id;
            if(!this.repository.Exist(dummyType)){
                throw new ExceptionController(LogicExceptions.INVALID_ID_TYPE);
            }
        }
    }
}
