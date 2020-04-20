using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System;
using IMMRequest.DataAccess.Interface;

namespace IMMRequest.BusinessLogic
{
    public class AreaLogic : BaseLogic<Area>
    {
        public IRepository<Area> areaRepository;

		public AreaLogic(IRepository<Area> areaRepository) 
        {
            this.areaRepository = areaRepository;
		}

        public AreaLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.areaRepository = new AreaRepository(IMMRequestContext);
		}

        public Guid Create(Area area) 
        {
            try {
                this.areaRepository.Add(area);
                this.areaRepository.Save();
                return area.Id;
            } 
            catch {
                throw new ArgumentException("Invalid guid");
            }
        }

        public override void Update(Area entity)
        {
            throw new NotImplementedException();
        }
    }
}