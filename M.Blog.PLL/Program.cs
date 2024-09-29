using M.Blog.DAL;
using M.Blog.BLL.Extensions;
using M.Blog.BLL.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));

builder.Services.AddDataAccess();
builder.Services.AddBusinessLogic(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Add services to the container.
//builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=GetUsers}/{id?}");

app.Run();
