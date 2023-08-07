using FurnitureAssemblyBusinessLogic.BusinessLogics;
using FurnitureAssemblyBusinessLogic.OfficePackage;
using FurnitureAssemblyBusinessLogic.OfficePackage.Implements;
using FurnitureAssemblyContracts.BusinessLogicContracts;
using FurnitureAssemblyContracts.StorageContracts;
using FurnitureAssemblyDatabaseImplement.Implements;
using FurnitureAssemblyWorkerClientApp;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IReportWorkerLogic, ReportWorkerLogic>();
builder.Services.AddTransient<ISetStorage, SetStorage>();
builder.Services.AddTransient<IFurnitureModuleStorage, FurnitureModuleStorage>();
builder.Services.AddTransient<IOrderInfoStorage, OrderInfoStorage>();
builder.Services.AddTransient<IOrderStorage, OrderStorage>();
builder.Services.AddTransient<AbstractWorkerSaveToExcel, SaveToExcel>();
builder.Services.AddTransient<AbstractWorkerSaveToPdf, SaveToPdf>();
builder.Services.AddTransient<AbstractWorkerSaveToWord, SaveToWord>();

builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
APIClient.Connect(builder.Configuration);
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Orders}/{id?}");

app.Run();
