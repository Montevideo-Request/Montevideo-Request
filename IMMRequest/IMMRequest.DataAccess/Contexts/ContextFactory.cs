using System;
using System.Linq;
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

        private static Random random = new Random();
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public IMMRequestContext CreateDbContext(string[] args) 
        {
            return GetNewContext();
        }

        public static IMMRequestContext GetNewContext(ContextType type = ContextType.SQL) 
        {
            var builder = new DbContextOptionsBuilder<IMMRequestContext>();
            
            return new IMMRequestContext(GetMemoryConfig(builder, GenerateRandomString(10)));
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
    }

}
