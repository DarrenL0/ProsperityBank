using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProsperityBank.BackgroundServices;
using ProsperityBank.Data;
using System;


namespace ProsperityBank
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
            services.AddDbContext<ProsperityBankDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(nameof(ProsperityBankDBContext)));

                // Enable lazy loading.
                options.UseLazyLoadingProxies();
            });
             
            //Store session into Web-Server memory
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // 5 min time out on user login
                options.IdleTimeout = TimeSpan.FromHours(1);
                //Make the session cookie essential
                options.Cookie.IsEssential = true;
            });


            services.AddHostedService<BillPayBackgroundService>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //error handling custom message
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
