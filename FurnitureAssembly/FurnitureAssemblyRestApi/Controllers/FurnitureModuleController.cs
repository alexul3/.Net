using DocumentFormat.OpenXml.Spreadsheet;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.BusinessLogicContracts;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDatabaseImplement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FurnitureAssemblyRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FurnitureModuleController : Controller
    {
        private readonly ILogger _logger;
        private readonly IFurnitureModuleLogic _furnitureModule;
        public FurnitureModuleController(ILogger<FurnitureModuleController> logger, IFurnitureModuleLogic furnitureModule)
        {
            _logger = logger;
            _furnitureModule = furnitureModule;
        }

        [HttpGet]
        public List<FurnitureModuleViewModel>? GetFurnitureModuleList()
        {
            try
            {
                return _furnitureModule.ReadList(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка мебельных модулей");
                throw;
            }
        }
        [HttpGet]
        public FurnitureModuleViewModel? GetFurnitureModule(int Id)
        {
            try
            {
                return _furnitureModule.ReadElement(new FurnitureModuleSearchModel { Id = Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения мебельного модуля по id={Id}", Id);
                throw;
            }
        }
        [HttpGet]
        public List<FurnitureModuleViewModel>? GetFurnitureModuleListByUser(int userId)
        {
            try
            {
                return _furnitureModule.ReadList(new FurnitureModuleSearchModel { UserId = userId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка гарнитуров у пользователя по id={userId}", userId);
                throw;
            }
        }
        [HttpPost]
        public void AddFurnitureModule(FurnitureModuleBindingModel model)
        {
            try
            {
                _furnitureModule.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания мебельного модуля");
                throw;
            }
        }
        [HttpPost]
        public void UpdateFurnitureModule(FurnitureModuleBindingModel model)
        {
            try
            {
                _furnitureModule.Update(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обновления мебельного модуля");
                throw;
            }
        }
        [HttpPost]
        public void DeleteFurnitureModule(FurnitureModuleBindingModel model)
        {
            try
            {
                _furnitureModule.Delete(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка удаления мебельного модуля");
                throw;
            }
        }
    }
}
