using FurnitureAssemblyBusinessLogic.BusinessLogics;
using FurnitureAssemblyBusinessLogic.OfficePackage.Implements.StoreKeeper;
using FurnitureAssemblyBusinessLogic.OfficePackage.StorekeeperSaveToFile;
using FurnitureAssemblyContracts.StorageContracts;
using FurnitureAssemblyDatabaseImplement.Implements;
using FurnitureAssemblyStoreKeeperClientApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IFurnitureStorage, FurnitureStorage> ();
builder.Services.AddTransient<IReportStorekeeperLogic, ReportStorekeeperLogic> ();

builder.Services.AddTransient<AbstractSaveToExcel, SaveStoreKeeperToExcel>();
builder.Services.AddTransient<AbstractSaveToPdf, SaveStoreKeeperToPdf>();
builder.Services.AddTransient<AbstractSaveToWord, SaveStoreKeeperToWord>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
