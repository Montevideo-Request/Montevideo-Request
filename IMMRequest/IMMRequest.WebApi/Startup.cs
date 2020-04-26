using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using IMMRequest.Domain;
using IMMRequest.DataAccess;
using IMMRequest.DataAccess.Interface;
using IMMRequest.BusinessLogic;
using IMMRequest.BusinessLogic.Interface;
using Microsoft.EntityFrameworkCore.Design;

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
            services.AddScoped<IRepository<Administrator>, AdministratorRepository>();
            services.AddScoped<ILogic<Administrator>, AdministratorLogic>();

            /* Area Settings */
            services.AddScoped<IRepository<Area>, AreaRepository>();
            services.AddScoped<ILogic<Area>, AreaLogic>();

            /*Topic Settings*/
            services.AddScoped<IRepository<Topic>, TopicRepository>();
            services.AddScoped<ILogic<Topic>, TopicLogic>();

            /* Type Settings */
            services.AddScoped<IRepository<TypeEntity>, TypeRepository>();
            services.AddScoped<ILogic<TypeEntity>, TypeLogic>();

            /* AdditionalField Settings */
            services.AddScoped<IRepository<AdditionalField>, AdditionalFieldRepository>();
            services.AddScoped<ILogic<AdditionalField>, AdditionalFieldLogic>();

            /* Request Settings */
            services.AddScoped<IRepository<Request>, RequestRepository>();
            services.AddScoped<ILogic<Request>, RequestLogic>();

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
