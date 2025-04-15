using DataAccessLayer.Models;
using ViewModels.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.BusinessLogic.Customers;
using Services.BusinessLogic.LandingPage;
using Services.BusinessLogic.AccountManagement;
using Services.BusinessLogic.Validations;
using System.Reflection;
using ViewModels.Infrastructure.Paging;
using System.Globalization;

namespace ViewModels
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<BankAppDataContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BankAppDataContext>();
            builder.Services.AddRazorPages();

            builder.Services.AddTransient<DataInitializer>();
            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddTransient<ICardsInfoService, CardsInfoService>();
            builder.Services.AddTransient<ICountryCardData, CountryCardData>();
            builder.Services.AddTransient<ZenQuotesService>();
            builder.Services.AddTransient<ICountryValidation, CountryValidation>();
            builder.Services.AddTransient<ICardsInfoService, CardsInfoService>();
            builder.Services.AddTransient<ITopTenService, TopTenService>();
            builder.Services.AddTransient<ITransactionDetailService, TransactionDetailService>();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.AddResponseCaching();



            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetService<DataInitializer>().SeedData();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseResponseCaching();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
