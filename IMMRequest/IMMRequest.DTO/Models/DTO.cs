using System.Collections.Generic;
using System.Linq;

namespace IMMRequest.DTO
{
    public abstract class DTO<E, M>
        where E : class
        where M : DTO<E, M>, new()
    {
        public static IEnumerable<M> ToModel(IEnumerable<E> entities)
        {
            return entities.Select(x => ToModel(x));
        }

        public static M ToModel(E entity)
        {
            if (entity == null) return null;
            return new M().SetModel(entity);
        }

        public static IEnumerable<E> ToEntity(IEnumerable<M> models)
        {
            return models.Select(x => ToEntity(x));
        }

        public static E ToEntity(M model)
        {
            if (model == null) return null;
            return model.ToEntity();
        }

        public abstract E ToEntity();

        protected abstract M SetModel(E entity);
    }
}
