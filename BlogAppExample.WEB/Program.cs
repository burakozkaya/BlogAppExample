using BlogAppExample.BLL.Extension;
using BlogAppExample.DAL.Extension;
using BlogAppExample.DTO.EMailConfigs;
using BlogAppExample.DTO.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    }
);
var conString = builder.Configuration.GetConnectionString("ConnectionStringBurak");
builder.Services.AddDALDependencies(conString);
builder.Services.AddBLLDependencies();
builder.Services.Configure<EMailOption>(builder.Configuration.GetSection("EmailOption"));
builder.Services.AddDtoDependencies();

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
