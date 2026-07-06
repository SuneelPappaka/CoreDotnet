using CoreDotnet.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<CoreDotnet.Services.IPaymentService, CoreDotnet.Services.PaypalServices>();
builder.Services.AddScoped<CoreDotnet.Services.IPaymentService, CoreDotnet.Services.RayZorPayServices>();
builder.Services.AddScoped<CoreDotnet.Services.INotificationService, CoreDotnet.Services.NotificationServicess>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoreDotnetDBConnection")));
builder.Services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//////////Session Configuration
builder.Services.AddDistributedMemoryCache(); // Add distributed memory cache for session state
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout to 30 minutes
    options.Cookie.HttpOnly = true; // Make the session cookie accessible only via HTTP
    options.Cookie.IsEssential = true; // Mark the session cookie as essential for GDPR compliance
});
////Session Configuration End
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();  
}
async Task SendRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "Admin","Manager", "User" };
    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}   
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SendRoles(services);
}
app.UseSession();// add session middleware to the request pipeline, enabling session state management for the application
app.UseStaticFiles();// allow serving static files from wwwroot folder
app.UseHttpsRedirection();// redirect HTTP requests to HTTPS
app.UseRouting();// add routing middleware where the routing decisions are made based on the incoming request

app.UseAuthorization();// add authorization middleware to enforce access control policies, check if the user is authorized to access the requested resource

app.MapStaticAssets();// map static assets to the request pipeline, allowing them to be served directly without going through the MVC pipeline

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
