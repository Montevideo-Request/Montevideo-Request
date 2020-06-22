using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
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
            EntityExist(entity);

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
            IsValidToUpdate(area);

            Area areaToUpdate = this.repository.Get(area.Id);
            areaToUpdate.Name =  area.Name;
            
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
                throw new ExceptionController(LogicExceptions.INVALID_NAME);
            }
        }

        public void IsValidToUpdate(Area area)
        { 
            if( (area.Name != null && area.Name.Length == 0) || area.Name == null )
            {
                throw new ExceptionController(LogicExceptions.INVALID_NAME);
            }

            if (this.repository.NameExists(area))
            {
                throw new ExceptionController(LogicExceptions.ALREADY_EXISTS_AREA + area.Name);   
            }
        }

        public void EntityExist(Area area)
        {
            if(this.repository.Exist(area)){
                throw new ExceptionController(LogicExceptions.ALREADY_EXISTS_AREA + area.Name);
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
