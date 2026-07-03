using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<CoreDotnet.Services.IPaymentService, CoreDotnet.Services.PaypalServices>();
builder.Services.AddScoped<CoreDotnet.Services.IPaymentService, CoreDotnet.Services.RayZorPayServices>();
builder.Services.AddScoped<CoreDotnet.Services.INotificationService, CoreDotnet.Services.NotificationServicess>();
builder.Services.AddDbContext<CoreDotnet.Data.ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoreDotnetDBConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();  
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
