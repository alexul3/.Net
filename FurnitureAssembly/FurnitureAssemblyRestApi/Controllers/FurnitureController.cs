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
    public class FurnitureController : Controller
    {
        private readonly ILogger _logger;
        private readonly IFurnitureLogic _furniture;
        public FurnitureController(ILogger<FurnitureController> logger, IFurnitureLogic furniture)
        {
            _logger = logger;
            _furniture = furniture;
        }

        [HttpGet]
        public List<FurnitureViewModel>? GetFurnitureList()
        {
            try
            {
                return _furniture.ReadList(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка мебели");
                throw;
            }
        }
        [HttpGet]
        public FurnitureViewModel? GetFurniture(int Id)
        {
            try
            {
                return _furniture.ReadElement(new FurnitureSearchModel { Id = Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения мебели по id={Id}", Id);
                throw;
            }
        }
        [HttpPost]
        public void AddFurniture(FurnitureBindingModel model)
        {
            try
            {
                _furniture.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания мебели");
                throw;
            }
        }
        [HttpPost]
        public void UpdateFurniture(FurnitureBindingModel model)
        {
            try
            {
                _furniture.Update(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обновления мебели");
                throw;
            }
        }
        [HttpPost]
        public void DeleteFurniture(FurnitureBindingModel model)
        {
            try
            {
                _furniture.Delete(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка удаления мебели");
                throw;
            }
        }
    }
}
