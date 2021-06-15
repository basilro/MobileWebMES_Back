using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using BackEnd.IDA;
using BackEnd.MSSQL;
using BackEnd.MW;
using BackEnd.Common;

namespace BackEnd.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            string root = configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
            Log.Init(root);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddCors();

            services
                .AddTransient<SecurityHelper>()
                .AddTransient<BasicMW>().AddTransient<IBasic, BasicDA>()
                .AddTransient<CommonMW>().AddTransient<ICommon, CommonDA>()
                .AddTransient<FileMW>().AddTransient<IFile, FileDA>();
                

            //대문자변경
            //services.AddMvc().AddJsonOptions(options =>
            //    options.SerializerSettings.ContractResolver = new DefaultContractResolver()
            //    );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        static void ConfigureNorthwindContext(IServiceProvider serviceProvider, DbContextOptionsBuilder options)
        {
            var hosting = serviceProvider.GetRequiredService<IHostingEnvironment>();
#if DB_LOCALDB
            var dbPath = Path.Combine(hosting.ContentRootPath, "Northwind.mdf");
            var connectionString = $@"Data Source=(localdb)\devextreme; AttachDbFileName={dbPath}; Integrated Security=True; MultipleActiveResultSets=True; App=EntityFramework";
            options.UseSqlServer(connectionString);
#endif

#if DB_SQLITE
            var dbPath = Path.Combine(hosting.ContentRootPath, "Northwind.sqlite");
            options.UseSqlite("Data Source=" + dbPath);
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(options => options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
