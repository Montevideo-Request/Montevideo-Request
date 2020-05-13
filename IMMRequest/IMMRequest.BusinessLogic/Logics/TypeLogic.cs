using IMMRequest.DataAccess;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
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

        public override TypeEntity Update(TypeEntity entity)
        {
            NotExist(entity.Id);
            TypeEntity typeEntityToUpdate = this.repository.Get(entity.Id);

            IsValidToUpdate(entity, typeEntityToUpdate);
            typeEntityToUpdate.Name = entity.Name;

            this.repository.Update(typeEntityToUpdate);
            this.repository.Save();
            
            return typeEntityToUpdate;
        }

        public override void Remove(Guid id)
        {
            NotExist(id);
            TypeEntity typeEntityToDelete = this.repository.Get(id);
            this.repository.Remove(typeEntityToDelete);
            this.repository.Save();
        }

        public override void IsValid(TypeEntity type)
        { 
            if((type.Name != null && type.Name.Length == 0) || type.Name == null)
            {
                throw new ExceptionController(LogicExceptions.INVALID_NAME);
            }
            if(ContainsType(type.Name, type.TopicId))
            {
                throw new ExceptionController(LogicExceptions.ALREADY_EXISTS_TYPE);
            }
        }

        public void IsValidToUpdate(TypeEntity type, TypeEntity typeEntityToUpdate)
        { 
            if( (type.Name != null && type.Name.Length == 0) || type.Name == null )
            {
                throw new ExceptionController(LogicExceptions.INVALID_NAME);
            }

            if (type.TopicId == null || (type.TopicId != null && type.TopicId.ToString().Length == 0))
            {
                throw new ExceptionController(LogicExceptions.INVALID_ID_TOPIC);  
            }

            if (typeEntityToUpdate.TopicId != type.TopicId)
            {
                throw new ExceptionController(LogicExceptions.INVALID_TOPIC_TYPE_COMBINATION);
            }

            if (this.repository.NameExists(type))
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
