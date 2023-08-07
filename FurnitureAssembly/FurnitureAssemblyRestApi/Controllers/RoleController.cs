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
    public class RoleController : Controller
    {
        private readonly ILogger _logger;
        private readonly IRoleLogic _role;
        public RoleController(ILogger<RoleController> logger, IRoleLogic role)
        {
            _logger = logger;
            _role = role;
        }

        [HttpGet]
        public List<RoleViewModel>? GetRoleList()
        {
            try
            {
                return _role.ReadList(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка мебельных модулей");
                throw;
            }
        }
        [HttpGet]
        public RoleViewModel? GetRole(int Id)
        {
            try
            {
                return _role.ReadElement(new RoleSearchModel { Id = Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения мебельного модуля по id={Id}", Id);
                throw;
            }
        }
        [HttpPost]
        public void AddRole(RoleBindingModel model)
        {
            try
            {
                _role.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания мебельного модуля");
                throw;
            }
        }
        [HttpPost]
        public void UpdateRole(RoleBindingModel model)
        {
            try
            {
                _role.Update(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обновления мебельного модуля");
                throw;
            }
        }
        [HttpPost]
        public void DeleteRole(RoleBindingModel model)
        {
            try
            {
                _role.Delete(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка удаления мебельного модуля");
                throw;
            }
        }
    }
}
