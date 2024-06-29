using DNTCaptcha.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Data.DataContext;
using PgrogrammingClass.Sevices.EntitesServices;
using ProgramingCalssProject.Models.Utillity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
    options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.SignIn.RequireConfirmedPhoneNumber = true;
        options.SignIn.RequireConfirmedEmail = false;
    }).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/AccessDenied";
    options.SlidingExpiration = true;
});


#region Myervices

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IAboutUsService, AboutUsService>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
builder.Services.AddScoped<IBannerService, BannerService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICityService, CityService>();

builder.Services.AddScoped<IContactUsService, ContactUsService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IProductCommentService, ProductCommentService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProvinceService, ProvinceService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<ISocialmediaService, SocialmediaService>();
builder.Services.AddScoped<IpersianDateTime, PersianDateTime>();
#endregion

builder.Services.AddDNTCaptcha(
    options =>
    {
        options.UseCookieStorageProvider()
.AbsoluteExpiration(minutes: 7)
.ShowThousandsSeparators(false)
.WithEncryptionKey("AAkhfldp==_lk!@#%^ydd74rr=")
.InputNames(// This is optional. Change it if you don't like the default names.
    new DNTCaptchaComponent
    {
        CaptchaHiddenInputName = "DNT_CaptchaText",
        CaptchaHiddenTokenName = "DNT_CaptchaToken",
        CaptchaInputName = "DNT_CaptchaInputText"
    }).Identifier("dnt_Captcha");
    });
builder.Services.AddScoped<IDNTCaptchaValidatorService, DNTCaptchaValidatorService>();
builder.Services.AddScoped<DNTCaptchaOptions,DNTCaptchaOptions>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{

    endpoints.MapControllerRoute(
          name: "Admin",
          pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();



});
app.MapRazorPages();
app.Run();
