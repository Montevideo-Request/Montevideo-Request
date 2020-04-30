using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using IMMRequest.BusinessLogic.Interface;
using System;
using System.Collections.Generic;

namespace IMMRequest.BusinessLogic
{
    public class AreaLogic : BaseLogic<Area>
    {
		public AreaLogic(IRepository<Area> areaRepository) 
        {
            this.repository = areaRepository;
		}

        public AreaLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.repository = new AreaRepository(IMMRequestContext);
		}

        public override void Update(Area entity)
        {
            throw new ExceptionController(ExceptionMessage.NOT_IMPLEMENTED);
            
        }

        public override void IsValid(Area area)
        { 
            if(area.Name.Length == 0)
            {
                throw new ExceptionController(ExceptionMessage.INVALID_LENGTH);
            }
            NotExist(area.Name);
        }

        private void NotExist(string name)
        {
            if (repository.Exist(a => a.Name == name))
            {
                throw new ExceptionController(ExceptionMessage.AREA_ALREADY_EXISTS);
            }
        }

        public override void EntityExists(Guid id)
        {
            if (!repository.Exist(a => a.Id == id))
            {
                throw new ExceptionController(ExceptionMessage.INVALID_AREA_ID);
            }
        }
    }
}
