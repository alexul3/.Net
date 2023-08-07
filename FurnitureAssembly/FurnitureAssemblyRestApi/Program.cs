using FurnitureAssemblyBusinessLogic.BusinessLogics;
using FurnitureAssemblyBusinessLogic.OfficePackage.Implements;
using FurnitureAssemblyBusinessLogic.OfficePackage;
using FurnitureAssemblyContracts.BusinessLogicContracts;
using FurnitureAssemblyContracts.StorageContracts;
using FurnitureAssemblyDatabaseImplement.Implements;
using Microsoft.OpenApi.Models;
using FurnitureAssemblyBusinessLogic.MailWorker;
using FurnitureAssemblyBusinessLogic.OfficePackage.StorekeeperSaveToFile;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Logging.AddLog4Net("log4net.config");

// Add services to the container.
builder.Services.AddTransient<IFurnitureModuleStorage, FurnitureModuleStorage>();
builder.Services.AddTransient<IFurnitureStorage, FurnitureStorage>();
builder.Services.AddTransient<IMaterialStorage, MaterialStorage>();
builder.Services.AddTransient<IOrderStorage, OrderStorage>();
builder.Services.AddTransient<IOrderInfoStorage, OrderInfoStorage>();
builder.Services.AddTransient<IRoleStorage, RoleStorage>();
builder.Services.AddTransient<IScopeStorage, ScopeStorage>();
builder.Services.AddTransient<ISetStorage, SetStorage>();
builder.Services.AddTransient<IUserStorage, UserStorage>();

builder.Services.AddTransient<IFurnitureModuleLogic, FurnitureModuleLogic>();
builder.Services.AddTransient<IFurnitureLogic, FurnitureLogic>();
builder.Services.AddTransient<IMaterialLogic, MaterialLogic>();
builder.Services.AddTransient<IOrderLogic, OrderLogic>();
builder.Services.AddTransient<IOrderInfoLogic, OrderInfoLogic>();
builder.Services.AddTransient<IRoleLogic, RoleLogic>();
builder.Services.AddTransient<IScopeLogic, ScopeLogic>();
builder.Services.AddTransient<ISetLogic, SetLogic>();
builder.Services.AddTransient<IUserLogic, UserLogic>();
builder.Services.AddTransient<IReportWorkerLogic, ReportWorkerLogic>();

builder.Services.AddTransient<IReportStorekeeperLogic, ReportStorekeeperLogic>();
builder.Services.AddTransient<MailWorker, MailWorker>();

builder.Services.AddTransient<AbstractWorkerSaveToExcel, SaveToExcel>();
builder.Services.AddTransient<AbstractWorkerSaveToWord, SaveToWord>();
builder.Services.AddTransient<AbstractWorkerSaveToPdf, SaveToPdf>();

builder.Services.AddTransient<AbstractSaveToExcel, FurnitureAssemblyBusinessLogic.OfficePackage.Implements.StoreKeeper.SaveStoreKeeperToExcel>();
builder.Services.AddTransient<AbstractSaveToWord, FurnitureAssemblyBusinessLogic.OfficePackage.Implements.StoreKeeper.SaveStoreKeeperToWord>();
builder.Services.AddTransient<AbstractSaveToPdf, FurnitureAssemblyBusinessLogic.OfficePackage.Implements.StoreKeeper.SaveStoreKeeperToPdf>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlacksmithWorkshopRestApi", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AbstractShopRestApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
