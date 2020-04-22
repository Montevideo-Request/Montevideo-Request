using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System;


namespace IMMRequest.BusinessLogic
{
    public class TypeLogic : BaseLogic<TypeEntity>
    {
        public TypeLogic(IRepository<TypeEntity> typeRepository) 
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
            throw new NotImplementedException();
        }
    }
}
