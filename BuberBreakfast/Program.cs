using BuberBreakfast.Data;
using BuberBreakfast.Services.Breakfasts;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                builder.Services.AddControllers();
                builder.Services.AddDbContext<BuberBreakfastContext>(options =>
                      options.UseSqlServer(builder.Configuration.GetConnectionString("BuberBreakfastDB")));
                builder.Services.AddScoped<IBreakfastService, DbBreakfastService>();
            }
            var app = builder.Build();

            {
                app.UseExceptionHandler("/error");
                app.UseHttpsRedirection();
                app.MapControllers();
                app.Run();
            }
        }
    }
}
