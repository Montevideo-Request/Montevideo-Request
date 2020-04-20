using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System;
using IMMRequest.DataAccess.Interface;

namespace IMMRequest.BusinessLogic
{
    public class TypeLogic : BaseLogic<TypeEntity>
    {
        public IRepository<TypeEntity> typeRepository;

        public TypeLogic(IRepository<TypeEntity> typeRepository) 
        {
            this.typeRepository = typeRepository;
        }

        public TypeLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.typeRepository = new TypeRepository(IMMRequestContext);
		}

        public Guid Create(TypeEntity type) 
        {
            try {
                this.typeRepository.Add(type);
                this.typeRepository.Save();
                return type.Id;
            } 
            catch {
                throw new ArgumentException("Invalid guid");
            }
        }

        public override void Update(TypeEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
