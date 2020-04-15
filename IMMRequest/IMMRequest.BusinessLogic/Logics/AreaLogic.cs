using  System.Collections.Generic;
using  IMMRequest.DataAccess;
using  IMMRequest.Domain;
using System.Linq;
using System;
using IMMRequest.BusinessLogic.Interface;

namespace IMMRequest.BusinessLogic
{
    public class AreaLogic : ILogic<Area>
    {
        public AreaRepository areaRepository;

        public AreaLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.areaRepository = new AreaRepository(IMMRequestContext);
		}

        public void Add(Area area)
        {
            this.areaRepository.Add(area);
        }

        public void Save()
        {
            this.areaRepository.Save();
        }        

        public Area Create(Area area) 
        {
            try
            {
                this.Add(area);
                return area;
            } 
            catch 
            {
                throw new ArgumentException("Id already exists");
            }
        }

        public void Remove(Guid id) 
        {
            try 
            {
                Area area = this.areaRepository.Get(id);
                this.areaRepository.Remove(area);
                this.areaRepository.Save();
            }
            catch
            {
                throw new ArgumentException("Invalid Id");
            }
        }

		public Area Get(Guid id) 
        {
            try
            {
                return this.areaRepository.Get(id);
            }
            catch 
            {
                throw new ArgumentException("Invalid Id");
            }
        }

        public IEnumerable<Area> GetAll() 
        {
            IEnumerable<Area> areas = this.areaRepository.GetAll();
            
            if (areas.Count() == 0) 
            {
                throw new ArgumentException("There are no Requests");
            }

            return areas;
		}

        public void Update(Area entity)
        {
            throw new NotImplementedException();
        }
    }
}