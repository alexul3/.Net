using DocumentFormat.OpenXml.Spreadsheet;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.BusinessLogicContracts;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDatabaseImplement.Models;
using FurnitureAssemblyDataModels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tuple = System.Tuple;

namespace FurnitureAssemblyRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SetController : Controller
    {
        private readonly ILogger _logger;
        private readonly ISetLogic _set;
        public SetController(ILogger<SetController> logger, ISetLogic set)
        {
            _logger = logger;
            _set = set;
        }

        [HttpGet]
        public List<SetViewModel>? GetSetList()
        {
            try
            {
                return _set.ReadList(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка мебельных модулей");
                throw;
            }
        }
        [HttpGet]
        public SetViewModel? GetSet(int Id)
        {
            try
            {
                return _set.ReadElement(new SetSearchModel { Id = Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения мебельного модуля по id={Id}", Id);
                throw;
            }
        }
        [HttpGet]
        public List<SetViewModel>? GetSetListByUser(int userId)
        {
            try
            {
                return _set.ReadList(new SetSearchModel { UserId = userId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка гарнитуров у пользователя по id={userId}", userId);
                throw;
            }
        }
        [HttpGet]
        public Tuple<SetViewModel, List<FurnitureModuleViewModel>, List<int>>? GetSetWithFurnitureModules(int setId)
        {
            try
            {
                var set = _set.ReadElement(new() { Id = setId });
                if (set == null)
                {
                    return null;
                }
                var tuple = Tuple.Create(set,
                    set.SetFurnitureModules.Select(x => new FurnitureModuleViewModel()
                    {
                        Id = x.Value.Item1.Id,
                        Cost = x.Value.Item1.Cost,
                        Name = x.Value.Item1.Name,
                        DateCreate = x.Value.Item1.DateCreate,
                    }).ToList(),
                    set.SetFurnitureModules.Select(x => x.Value.Item2).ToList());
                return tuple;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка гарнитура с мебельными модулями");
                throw;
            }
        }
        [HttpGet]
        public Dictionary<int, (IFurnitureModuleModel, int)>? GetSetFurnitureModules(int setId)
        {
			try
			{
				var set = _set.ReadElement(new() { Id = setId });
				if (set == null)
				{
					return null;
				}
				return set.SetFurnitureModules;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка мебельных модулей поездок гарнитура");
				throw;
			}
		}
		[HttpPost]
        public SetViewModel? AddSet(SetBindingModel model)
        {
            try
            {
                return _set.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания мебельного модуля");
                throw;
            }
        }
        [HttpPost]
        public void UpdateSet(SetBindingModel model)
        {
            try
            {
                _set.Update(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обновления мебельного модуля");
                throw;
            }
        }
        [HttpPost]
        public void DeleteSet(SetBindingModel model)
        {
            try
            {
                _set.Delete(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка удаления мебельного модуля");
                throw;
            }
        }
        [HttpPost]
        public void AddFurnitureModuleInSet(Tuple<SetSearchModel, FurnitureModuleViewModel, int> setFurnitureModuleWithCount)
        {
            try
            {
                _set.AddFurnitureModuleInSet(setFurnitureModuleWithCount.Item1, setFurnitureModuleWithCount.Item2, setFurnitureModuleWithCount.Item3);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка добавления поездки в магазин");
                throw;
            }
        }
    }
}
