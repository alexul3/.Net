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
    public class ScopeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IScopeLogic _scope;
        public ScopeController(ILogger<ScopeController> logger, IScopeLogic scope)
        {
            _logger = logger;
            _scope = scope;
        }

        [HttpGet]
        public List<ScopeViewModel>? GetScopeList()
        {
            try
            {
                return _scope.ReadList(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка областей");
                throw;
            }
        }
        [HttpGet]
        public ScopeViewModel? GetScope(int Id)
        {
            try
            {
                return _scope.ReadElement(new ScopeSearchModel { Id = Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения области по id={Id}", Id);
                throw;
            }
        }
        [HttpPost]
        public void AddScope(ScopeBindingModel model)
        {
            try
            {
                _scope.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания области");
                throw;
            }
        }
        [HttpPost]
        public void UpdateScope(ScopeBindingModel model)
        {
            try
            {
                _scope.Update(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обновления области");
                throw;
            }
        }
        [HttpPost]
        public void DeleteScope(ScopeBindingModel model)
        {
            try
            {
                _scope.Delete(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка удаления области");
                throw;
            }
        }
    }
}
