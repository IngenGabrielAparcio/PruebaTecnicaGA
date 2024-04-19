using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Prueba.BL.Services;
using Prueba.Core.Interfaces;
using Prueba.Core.Utilities;
using Prueba.infraestructure.Access;
using Prueba.infraestructure.Data;

namespace Prueba.Api
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

            services.AddTransient<ICatalogServices, CatalogServices>();
            services.AddTransient<ICatalogDataAccess, CatalogDataAccess>();

            services.AddTransient<ILoginServices, LoginServices>();
            services.AddTransient<ILoginDataAccess, LoginDataAccess>();

            services.AddTransient<IProductsServices, ProductsServices>();
            services.AddTransient<IProductsDataAccess, ProductsDataAccess>();

            services.AddTransient<IOrderServices, OrderServices>();
            services.AddTransient<IOrderDataAccess, OrderDataAccess>();

            // Configuracion Automapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GlobalMapper());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Prueba.Api", Version = "v1" });
            });

            services.AddDbContext<DBStoreTestContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DataBase")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba.Api v1"));

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
