using Microsoft.EntityFrameworkCore;
using DataAccess;
using Domain;

var builder = WebApplication.CreateBuilder(args);

//Comment one or the other to display data from database or json file
builder.Services.AddScoped<IPollRepository, PollRepository>(); //Datasbe
//builder.Services.AddSingleton<IPollRepository, PollFileRepository>(); //Json
builder.Services.AddDbContext<PollDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PollDb")));

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Poll}/{action=Index}/{id?}");

app.Run();
