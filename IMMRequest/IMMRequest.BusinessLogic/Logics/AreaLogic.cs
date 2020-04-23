using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using IMMRequest.Exceptions;

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
            return ;
        }
    }
}
