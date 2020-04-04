using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IMMRequest.DataAccess 
{
    public enum ContextType 
    {
        MEMORY, SQL
    }

    public class ContextFactory 
    {

        public static string InMemoryDBName = "IMMRequestDB";

        public IMMRequestContext CreateDbContext(string[] args) 
        {
            return GetNewContext();
        }

        public static IMMRequestContext GetNewContext(ContextType type = ContextType.SQL) 
        {
            var builder = new DbContextOptionsBuilder<IMMRequestContext>();
            /*DbContextOptions options = null;
            if (type == ContextType.MEMORY) {
                options = GetMemoryConfig(builder, InMemoryDBName);
            } else {
                options = GetSqlConfig(builder);
            }
            //return new IMMRequestContext(options);*/
            return new IMMRequestContext(GetMemoryConfig(builder, InMemoryDBName));
        }

        public static IMMRequestContext GetMemoryContext(string DbName) 
        {
            var builder = new DbContextOptionsBuilder<IMMRequestContext>();
            return new IMMRequestContext(GetMemoryConfig(builder, DbName));
        }

        private static DbContextOptions GetMemoryConfig(DbContextOptionsBuilder builder, string DbName) 
        {
            builder.UseInMemoryDatabase(DbName);
            return builder.Options;
        }

        /*private static DbContextOptions GetSqlConfig(DbContextOptionsBuilder builder) 
        {
            //TODO: Se puede mejorar esto colocando en un archivo externo y obteniendo
            //      desde allí la información.
            builder.UseSqlServer(@"Server=127.0.0.1,1433;Database=HomeworksDB;User Id=sa;Password=Abcd1234;");
            return builder.Options;
        }*/
    }

}
