using IMMRequest.BusinessLogic.Interface;
using IMMRequest.DataAccess.Interface;
using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using IMMRequest.Exceptions;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic
{
    public class AreaLogic : IAreaLogic<Area>
    {
        protected IAreaRepository<Area> repository;
		public AreaLogic(IAreaRepository<Area> areaRepository) 
        {
            this.repository = areaRepository;
		}

        public AreaLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.repository = new AreaRepository(IMMRequestContext);
		}

        public Area Create(Area entity)
        {
            IsValid(entity);
            this.repository.Add(entity);
            this.repository.Save();
            return entity;
        }

        public Area Get(Guid id)
        {
            NotExist(id);
            return this.repository.Get(id);
        }

        public IEnumerable<Area> GetAll()
        {
            IEnumerable<Area> entities = this.repository.GetAll();
            
            if (entities.Count() == 0) 
            {
                throw new ExceptionController(LogicExceptions.GENERIC_NO_ELEMENTS);
            }

            return entities;
		}

        public void Save()
        {
            this.repository.Save();
        }

        public Area Update(Area area)
        {
            NotExist(area.Id);
            Area areaToUpdate = this.repository.Get(area.Id);
            areaToUpdate.Name = area.Name != null ? area.Name : areaToUpdate.Name;
            
            this.repository.Update(areaToUpdate);
            this.repository.Save();

            return areaToUpdate;
        }

        public void Remove(Guid id)
        {
            NotExist(id);
            Area areaToDelete = this.repository.Get(id);
            this.repository.Remove(areaToDelete);
            this.repository.Save();
        }

        public void IsValid(Area area)
        { 
            if( (area.Name != null && area.Name.Length == 0) || area.Name == null )
            {
                throw new ExceptionController(LogicExceptions.INVALID_LENGTH);
            }
            Area dummyArea = new Area();
            dummyArea.Name = area.Name;
            EntityExist(dummyArea);
        }

        public void EntityExist(Area area)
        {
            if(this.repository.Exist(area)){
                throw new ExceptionController(LogicExceptions.ALREADY_EXISTS_AREA);
            }
        }
        
        public void NotExist(Guid id)
        {
            Area dummyArea = new Area();
            dummyArea.Id = id;
            if(!this.repository.Exist(dummyArea)){
                throw new ExceptionController(LogicExceptions.INVALID_ID_AREA);
            }
        }
    }
}
