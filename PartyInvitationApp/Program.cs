//DRASTI PATEL
//PROBLEM ANALYSIS 2
//MARCH 09, 2025

using Microsoft.EntityFrameworkCore;
using PartyInvitationApp.Data;

var builder = WebApplication.CreateBuilder(args);   //Creates a new instance of the web application builder

//Adds Razor Pages support for rendering views
builder.Services.AddRazorPages();

// Adds support for MVC Controllers and Views
builder.Services.AddControllersWithViews();

//Configures the database context using SQLite as the database provider
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=partyinvites.db")); // Using SQLite    //Database file: partyinvites.db

//Registers IHttpContextAccessor to allow access to HTTP context across the application
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();    //builds the application

app.UseHttpsRedirection();           //Enables HTTPS redirection for security
app.UseStaticFiles();                //Serves static files (CSS, JS, images, etc.)
app.UseRouting();                    //Configures routing to map incoming requests to the correct handlers
app.UseAuthorization();              //Enables authentication and authorization mechanisms
app.MapRazorPages();                 //Maps Razor Pages

//Defines the default route pattern for controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"     // Default route to HomeController
);

app.Run();