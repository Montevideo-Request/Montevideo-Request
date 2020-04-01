using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IMMRequest.DataAccess
{

    public class ContextFactory
    {
        public static IMMRequestContext GetMemoryContext(string nameBd) {
            var builder = new DbContextOptionsBuilder<IMMRequestContext>();
            return new IMMRequestContext(GetMemoryConfig(builder, nameBd));
        }

        private static DbContextOptions GetMemoryConfig(DbContextOptionsBuilder builder, string nameBd) {
            builder.UseInMemoryDatabase(nameBd);
            return builder.Options;
        }
    }

}
