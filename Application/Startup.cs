using AutoMapper;
using AutoMapper.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using IShipmentServiceDal = DAL.IShipmentService;
using ITransactionServiceBll = BLL.ITransactionService;
using ITransactionServiceDal = DAL.ITransactionService;
using ShipmentServiceDal = DAL.ShipmentService;
using TransactionServiceBll = BLL.TransactionService;
using TransactionServiceDal = DAL.TransactionService;

namespace Application
{
    public class Startup
    {
        private bool IsAutomapperInitialzed { get; set; } = false;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            InitializeAutomapper();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IShipmentServiceDal, ShipmentServiceDal>();
            services.AddTransient<ITransactionServiceDal, TransactionServiceDal>();
            services.AddTransient<ITransactionServiceBll, TransactionServiceBll>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ApplicationEnvironment.InputFilePath = Configuration["FilePath"];
                        
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }

        private void InitializeAutomapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddConditionalObjectMapper().Where((s, d) => s.Name == d.Name + "Dto");
            });
        }
    }
}