using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using TestDevCom.Web.Services;

var builder = WebApplication.CreateBuilder(args);
var apiBaseUrl = builder.Configuration["ApiBaseUrl"];
var googleAuth = builder.Configuration.GetSection("Authentication:Google");


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("TestDevComAPI", client =>
{   
    client.BaseAddress = new Uri(apiBaseUrl!);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = googleAuth["ClientId"]!;
    options.ClientSecret = googleAuth["ClientSecret"]!;
    options.CallbackPath = googleAuth["CallbackPath"];
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/signin-google";
});

builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
