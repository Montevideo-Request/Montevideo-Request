using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using IMMRequest.BusinessLogic;
using IMMRequest.DataAccess;
using IMMRequest.Domain;

namespace IMMRequest.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            /* DataBase Settings */
            services.AddDbContext<DbContext, IMMRequestContext>(
                o => o.UseSqlServer(Configuration.GetConnectionString("IMMRequestDB"))
            );

            /* Administrator Settings */
            services.AddScoped<IAdministratorRepository<Administrator>, AdministratorRepository>();
            services.AddScoped<IAdministratorLogic<Administrator>, AdministratorLogic>();

            /* Area Settings */
            services.AddScoped<IAreaRepository<Area>, AreaRepository>();
            services.AddScoped<IAreaLogic<Area>, AreaLogic>();

            /*Topic Settings*/
            services.AddScoped<IRepository<Topic, Area>, TopicRepository>();
            services.AddScoped<ILogic<Topic>, TopicLogic>();

            /* Type Settings */
            services.AddScoped<IRepository<TypeEntity, Topic>, TypeRepository>();
            services.AddScoped<ILogic<TypeEntity>, TypeLogic>();

            /* AdditionalField Settings */
            services.AddScoped<IRepository<AdditionalField, TypeEntity>, AdditionalFieldRepository>();
            services.AddScoped<IAdditionalFieldLogic<AdditionalField, FieldRange>, AdditionalFieldLogic>();

            /* Request Settings */
            services.AddScoped<IRequestRepository<Request, TypeEntity>, RequestRepository>();
            services.AddScoped<IRequestLogic<Request, TypeEntity>, RequestLogic>();

            /* Session Settings */
            services.AddScoped<ISessionLogic, SessionLogic>();

            /* Parser Settings */
            services.AddScoped<IParserLogic, ParserLogic>();

            /* Ignore NULL values on WebApi JSON */
            services.AddMvc().AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.IgnoreNullValues = true;
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
