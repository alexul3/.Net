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
    public class MaterialController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMaterialLogic _material;
        public MaterialController(ILogger<MaterialController> logger, IMaterialLogic material)
        {
            _logger = logger;
            _material = material;
        }

        [HttpGet]
        public List<MaterialViewModel>? GetMaterialList()
        {
            try
            {
                return _material.ReadList(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка материалов");
                throw;
            }
        }
        [HttpGet]
        public MaterialViewModel? GetMaterial(int Id)
        {
            try
            {
                return _material.ReadElement(new MaterialSearchModel { Id = Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения материала по id={Id}", Id);
                throw;
            }
        }
        [HttpPost]
        public void AddMaterial(MaterialBindingModel model)
        {
            try
            {
                _material.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания материала");
                throw;
            }
        }
        [HttpPost]
        public void UpdateMaterial(MaterialBindingModel model)
        {
            try
            {
                _material.Update(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обновления материала");
                throw;
            }
        }
        [HttpPost]
        public void DeleteMaterial(MaterialBindingModel model)
        {
            try
            {
                _material.Delete(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка удаления материала");
                throw;
            }
        }
    }
}
