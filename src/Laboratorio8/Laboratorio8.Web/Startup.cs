using Laboratorio8.Moduli;
using Laboratorio8.Web.Infrastructure;
using Laboratorio8.Web.SignalR.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Laboratorio8.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();

            services.AddAntiforgery(opts => opts.Cookie.Name = "AntiForgery.LaboratorioFrontend");
            services.AddDataProtection(opts => opts.ApplicationDiscriminator = "LaboratorioFrontend");

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
            });

            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            services.Configure<AppSettings>(appSettingsSection);

            services.AddDbContext<ModuliDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: "Moduli");
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Login/Index";
                options.LogoutPath = "/Login/Logout";
            });

            var mvcBuilder = services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                { // Enable loading SharedResource for ModelLocalizer
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                });

#if DEBUG
            mvcBuilder.AddRazorRuntimeCompilation();
#endif

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/Areas/{2}/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");

                options.ViewLocationFormats.Clear();
                options.ViewLocationFormats.Add("/Features/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Features/Views/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Features/Views/Shared/{0}.cshtml");
                options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

            services.AddSignalR();

            Container.RegisterTypes(services);
            WebContainer.RegisterTypes(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.Use(async (ctx, next) =>
                {
                    await next();
                    if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
                    {
                        //Re-execute the request so the user gets the error page
                        string originalPath = ctx.Request.Path.Value;
                        ctx.Items["originalPath"] = originalPath;
                        ctx.Request.Path = "/Home/PageNotFound";
                        await next();
                    }
                });
                app.UseHsts();
            }

            app.UseRequestLocalization(SupportedCultures.CultureNames);

            app.UseRouting();

            app.UseResponseCompression();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            var node_modules = new CompositePhysicalFileProvider(Directory.GetCurrentDirectory(), "node_modules");
            var areas = new CompositePhysicalFileProvider(Directory.GetCurrentDirectory(), "Areas");
            var features = new CompositePhysicalFileProvider(Directory.GetCurrentDirectory(), "Features");
            var compositeFp = new CustomCompositeFileProvider(env.WebRootFileProvider, node_modules, areas, features);
            env.WebRootFileProvider = compositeFp;  //For file versioning
            app.UseStaticFiles();                   //Implicit use of env.WebRootFileProvider

            // Implementa il problem details sulle action MVC che non sono WebApi
            app.UseWhen(context => context.Request.IsAjaxRequest(), a => a.UseExceptionHandler(a => a.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = feature.Error;

                var problemDetails = new ProblemDetails
                {
                    Status = 500,
                    Title = exception.Message,
                    Detail = exception.ToString(),
                    Type = null,
                };

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.ToJsonCamelCase(problemDetails));
            })));

            app.UseEndpoints(routes =>
            {
                routes.MapAreaControllerRoute("Configurazioni", "Configurazioni", "Configurazioni/{controller=MetaModuli}/{action=Index}/{id?}");
                routes.MapAreaControllerRoute("Compilazioni", "Compilazioni", "Compilazioni/{controller=Moduli}/{action=Edit}/{id?}");

                routes.MapHub<ConfigurazioneMetaModuliHub>("/configurazioneMetaModuliHub");

                routes.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
        }
    }

    public static class SupportedCultures
    {
        public readonly static string[] CultureNames;
        public readonly static CultureInfo[] Cultures;

        static SupportedCultures()
        {
            CultureNames = new[] { "it-it" };
            Cultures = CultureNames.Select(c => new CultureInfo(c)).ToArray();

            //NB: attenzione nel progetto a settare correttamente <NeutralLanguage>it-IT</NeutralLanguage>
        }
    }
}