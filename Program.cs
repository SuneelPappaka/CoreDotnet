using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<CoreDotnet.Services.IPaymentService, CoreDotnet.Services.PaypalServices>();
builder.Services.AddScoped<CoreDotnet.Services.IPaymentService, CoreDotnet.Services.RayZorPayServices>();
builder.Services.AddScoped<CoreDotnet.Services.INotificationService, CoreDotnet.Services.NotificationServicess>();
builder.Services.AddDbContext<CoreDotnet.Data.ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoreDotnetDBConnection")));
builder.Services.AddIdentity<CoreDotnet.Data.ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>()
    .AddEntityFrameworkStores<CoreDotnet.Data.ApplicationDbContext>()
    .AddDefaultTokenProviders();
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
app.UseStaticFiles();// allow serving static files from wwwroot folder
app.UseHttpsRedirection();// redirect HTTP requests to HTTPS
app.UseRouting();// add routing middleware where the routing decisions are made based on the incoming request

app.UseAuthorization();// add authorization middleware to enforce access control policies, check if the user is authorized to access the requested resource

app.MapStaticAssets();// map static assets to the request pipeline, allowing them to be served directly without going through the MVC pipeline

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
