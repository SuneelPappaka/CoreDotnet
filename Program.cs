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
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
