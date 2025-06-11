using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddHttpClient("BackendAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5106/");
});

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/SignIn";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
    });

var app = builder.Build();

app.UseExceptionHandler("/Error");
app.UseStaticFiles();
app.UseRouting();

// must come before MapRazorPages()
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.Run();
