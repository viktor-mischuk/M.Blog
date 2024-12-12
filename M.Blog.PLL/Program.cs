using M.Blog.DAL;
using M.Blog.BLL.Extensions;
using M.Blog.BLL.Services;
using NLog.Web;
using NLog;


var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddDataAccess();
    builder.Services.AddBusinessLogic(builder.Configuration);
    builder.Services.AddControllersWithViews();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();
    app.Environment.EnvironmentName = "Production"; // меняем имя окружения

    //Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/SomethingWrong");//Глобальный обработчик
        app.UseHsts();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseStaticFiles();


    //app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch(Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}


