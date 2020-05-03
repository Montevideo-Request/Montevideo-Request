using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using IMMRequest.Exceptions;
using System;

namespace IMMRequest.BusinessLogic
{
    public class AreaLogic : BaseLogic<Area, Area>
    {
		public AreaLogic(IRepository<Area, Area> areaRepository) 
        {
            this.repository = areaRepository;
		}

        public AreaLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.repository = new AreaRepository(IMMRequestContext);
		}

        public override void Update(Area area)
        {
            NotExist(area.Id);
            Area areaToUpdate = this.repository.Get(area.Id);
            areaToUpdate.Name = area.Name;
            areaToUpdate.Topics = area.Topics;
            this.repository.Update(areaToUpdate);
            this.repository.Save();
        }

        public override void Remove(Area area)
        {
            NotExist(area.Id);
            this.repository.Remove(area);
            this.repository.Save();
        }

        public override void IsValid(Area area)
        { 
            if(area.Name != null && area.Name.Length == 0)
            {
                throw new ExceptionController(LogicExceptions.INVALID_LENGTH);
            }
            Area dummyArea = new Area();
            dummyArea.Name = area.Name;
            EntityExist(dummyArea);
        }

        public override void EntityExist(Area area)
        {
            if(this.repository.Exist(area)){
                throw new ExceptionController(LogicExceptions.ALREADY_EXISTS_AREA);
            }
        }
        
        public override void NotExist(Guid id)
        {
            Area dummyArea = new Area();
            dummyArea.Id = id;
            if(!this.repository.Exist(dummyArea)){
                throw new ExceptionController(LogicExceptions.INVALID_ID_AREA);
            }
        }
    }
}
