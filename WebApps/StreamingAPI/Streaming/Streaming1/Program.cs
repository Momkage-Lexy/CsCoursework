using Streaming1.DAL.Abstract;
using Streaming1.DAL.Concrete;
using Streaming1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Streaming1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();


        var connectionString = builder.Configuration.GetConnectionString("StreamingConnection");
        builder.Services.AddDbContext<StreamingDbContext>(options => options
                                    .UseLazyLoadingProxies()    // Will use lazy loading, but not in LINQPad as it doesn't run Program.cs
                                    .UseSqlServer(
                                        builder.Configuration.GetConnectionString("StreamingConnection")));
                                        builder.Services.AddScoped<DbContext,StreamingDbContext>();
                                        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                                        builder.Services.AddScoped<IShowRepository, ShowRepository>();
                                        builder.Services.AddScoped<IAgeCertificationRepository, AgeCertificationRepository>();
                                        builder.Services.AddScoped<ICreditRepository, CreditRepository>();
                                        builder.Services.AddScoped<IGenreRepository, GenreRepository>();
                                        builder.Services.AddScoped<IGenreAssignmentRepository, GenreAssignmentRepository>();
                                        builder.Services.AddScoped<IPersonRepository, PersonRepository>();
                                        builder.Services.AddScoped<IProductionCountryRepository, ProductionCountryRepository>();
                                        builder.Services.AddScoped<IProductionCountryAssignmentRepository, ProductionCountryAssignmentRepository>();
                                        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
                                        builder.Services.AddScoped<IShowTypeRepository, ShowTypeRepository>();
                                 
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger();   
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
