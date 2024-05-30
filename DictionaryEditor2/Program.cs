using Microsoft.EntityFrameworkCore;
using DictionaryEditor.Db;
using DictionaryEditor.Db.Models;
using Microsoft.AspNetCore.Identity;
using DictionaryEditor2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connection = builder.Configuration.GetConnectionString("dictionary_json");

//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DatabaseContext>();

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.ExpireTimeSpan = TimeSpan.FromDays(30);
//    options.LoginPath = "/Account/Login";
//    options.LogoutPath = "/Account/Logout";
//    options.Cookie = new CookieBuilder
//    {
//        IsEssential = true
//    };
//});

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));
builder.Services.AddTransient<DictionariesRepository>();
builder.Services.AddTransient<RussianWordsDbRepository>();
builder.Services.AddTransient<OssetianWordsDbRepository>();
builder.Services.AddTransient<ExamplesDbRepository>();
builder.Services.AddTransient<UserDbRepository>();
builder.Services.AddTransient<RoleDbRepository>();
builder.Services.AddTransient<RusWordsHashSet>();


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

//app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
