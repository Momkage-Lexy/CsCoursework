using Homework4.Services;
using System.Net.Http.Headers;
namespace Homework4 {
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string tmdbAPIKey = builder.Configuration["TMDBApiKey"];
            string tmdbAPIUrl = "https://api.themoviedb.org/3";

            builder.Services.AddHttpClient<ITmdbService, TmdbService>((httpClient, services) => 
            {
                httpClient.BaseAddress = new Uri(tmdbAPIUrl);
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tmdbAPIKey);
                return new TmdbService(httpClient, services.GetRequiredService<ILogger<TmdbService>>());
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(); // You can optionally specify a custom route here
            }

           
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

