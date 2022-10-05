/*The first section imports some namespaces*/
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NorthwingMvc.Data;



/*The second section creates and configures a web host builder.
 It registers an application database context using SQL Server or SQLite 
 with its database connection string loaded from the appsettings.json file 
 for its data storage, adds ASP.NET Core Identity for authentication and 
 configures it to use the application database, and adds support for MVC controllers 
 with views, as shown in the following code:*/
// Add services to the container.

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

/*The call to AddDbContext is an example of registering a dependency service.
 ASP.NET Core implements the dependency injection (DI) design pattern so
  that other components like controllers can request needed services through their
   constructors. Developers register those services in this section of Program.cs
    (or if using a Startup class then in its ConfigureServices method.)*/

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

/*filters can be applied at the global level by
 adding the attribute type to the Filters collection of the MvcOptions instance 
 that can be used to configure MVC 
 when calling the AddControllersWithViews method, 
 as shown in the following code*/

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(MyCustomFilter));
});

/*The builder object has two commonly used objects: Configuration and Services:

• Configuration contains merged values from all the places you could set configuration: appsettings.json, environment variables, command-line arguments, and so on

• Services is a collection of registered dependency services*/

var app = builder.Build();

// Configure the HTTP request pipeline.
/*The third section configures the HTTP request pipeline.
 It configures a relative URL path to run database migrations if 
 the website runs in development, or a friendlier error page and 
 HSTS for production. HTTPS redirection, static files, routing, and 
 ASP.NET Identity are enabled, and an MVC default route and Razor Pages are configured,*/
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

/*Apart from the UseAuthentication and UseAuthorization methods,
 the most important new method in this section of Program.cs is MapControllerRoute, 
 which maps a default route for use by MVC. This route is very flexible because 
 it will map to almost any incoming URL, as you will see in the next topic.*/
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

/*Although we will not create any Razor Pages in this chapter,
 we need to leave the method call that maps Razor Page 
 support because our MVC website uses ASP.NET Core Identity 
 for authentication and authorization, and it uses a Razor Class Library 
 for its user interface components, like visitor registration and login.*/
app.MapRazorPages();

/*The fourth and final section has a thread-blocking method
 call that runs the website and waits for incoming HTTP requests to 
 respond to, as shown in the following code:*/
app.Run();

public class MyCustomFilter { }
