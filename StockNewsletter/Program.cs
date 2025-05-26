using StockNewsletter.Services;
using Microsoft.EntityFrameworkCore;
using StockNewsletter.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache(); // Add memory caching for AlphaVantage API responses
builder.Services.AddScoped<AlphaVantageService>();
builder.Services.AddDbContext<StockNewsletter.Data.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Add basic request logging (simplified)
app.Use(async (context, next) =>
{
    var request = context.Request;
    Console.WriteLine($"🌐 REQUEST: {request.Method} {request.Path} - User: {context.User.Identity?.Name ?? "Anonymous"}");
    
    await next();
    
    Console.WriteLine($"🔙 RESPONSE: {context.Response.StatusCode} for {request.Method} {request.Path}");
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
