using BlogAppExample.BLL.Extension;
using BlogAppExample.DAL.Extension;
using BlogAppExample.DTO.EMailConfigs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var conString = builder.Configuration.GetConnectionString("DevConnectionString");
builder.Services.AddDALDependencies(conString);
builder.Services.AddBLLDependencies();
builder.Services.Configure<EMailOption>(builder.Configuration.GetSection("EmailOption"));

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();