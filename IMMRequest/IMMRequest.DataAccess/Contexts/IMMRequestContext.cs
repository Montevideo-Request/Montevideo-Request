using  Microsoft.EntityFrameworkCore;
using  IMMRequest.Domain;

namespace  IMMRequest.DataAccess
{
	public  class  IMMRequestContext : DbContext
	{
        public DbSet<AdditionalField> AdditionalFields { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<AdditionalFieldRange> AdditionalFieldRanges { get; set; }
        public DbSet<AreaTopic> AreaTopics { get; set; }
        public DbSet<TopicType> TopicTypes { get; set; }
        public DbSet<TypeAdditionalFields> TypeAdditionalFields { get; set; }
        

		public  IMMRequestContext(DbContextOptions  options) : base(options) { }
	}
}
