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
    public class UserController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserLogic _user;
        public UserController(ILogger<UserController> logger, IUserLogic user)
        {
            _logger = logger;
            _user = user;
        }

        [HttpGet]
        public List<UserViewModel>? GetUserList()
        {
            try
            {
                return _user.ReadList(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка мебельных модулей");
                throw;
            }
        }
        [HttpGet]
        public UserViewModel? GetUser(int Id)
        {
            try
            {
                return _user.ReadElement(new UserSearchModel { Id = Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения мебельного модуля по id={Id}", Id);
                throw;
            }
        }
        [HttpGet]
        public UserViewModel? Login(string login, string password)
        {
            try
            {
                return _user.ReadElement(new UserSearchModel
                {
                    Login = login,
                    Password = password
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка входа в систему");
                throw;
            }
        }
        [HttpPost]
        public void AddUser(UserBindingModel model)
        {
            try
            {
                _user.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания мебельного модуля");
                throw;
            }
        }
        [HttpPost]
        public void UpdateUser(UserBindingModel model)
        {
            try
            {
                _user.Update(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обновления мебельного модуля");
                throw;
            }
        }
        [HttpPost]
        public void DeleteUser(UserBindingModel model)
        {
            try
            {
                _user.Delete(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка удаления мебельного модуля");
                throw;
            }
        }
    }
}
