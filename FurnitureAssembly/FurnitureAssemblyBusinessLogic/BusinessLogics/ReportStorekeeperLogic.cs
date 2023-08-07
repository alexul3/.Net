using FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels.StorekeeperModels;
using FurnitureAssemblyBusinessLogic.OfficePackage.StorekeeperSaveToFile;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.StorageContracts;
using FurnitureAssemblyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyBusinessLogic.BusinessLogics
{
    public class ReportStorekeeperLogic : IReportStorekeeperLogic
    {
        private readonly IFurnitureStorage _furnitureStorage;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToPdf _saveToPdf;

        public ReportStorekeeperLogic(IFurnitureStorage furnitureStorage,
            AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord, AbstractSaveToPdf saveToPdf)
        {
            _furnitureStorage = furnitureStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }
        public List<ReportFurnitureMaterialViewModel> GetFurnitureComponent(int[]? ids)
        {
            var furnitures = _furnitureStorage.GetFullList().Where(x => ids.Contains(x.Id));
            if(ids == null)
            {
                furnitures = _furnitureStorage.GetFullList();
            }
            var list = new List<ReportFurnitureMaterialViewModel>();
            foreach (var furniture in furnitures)
            {
                var record = new ReportFurnitureMaterialViewModel
                {
                    FurnitureName = furniture.Name,
                    Materials = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var materials in furniture.FurnitureMaterials.Values)
                {
                    record.Materials.Add(new Tuple<string, int>(materials.Item1.Name, materials.Item2));
                    record.TotalCount += materials.Item2;
                }
                list.Add(record);
            }
            return list;
        }

        public List<ReportMaterialsViewModel> GetOrders(ReportBindingModel model)
        {
            var furnitures = _furnitureStorage.GetFilteredList(new FurnitureSearchModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            }).ToList();
            List<MaterialViewModel> orders = new List<MaterialViewModel>();
            foreach(var furniture in furnitures)
            {
                var elemList = furniture.FurnitureMaterials;
                var elem = elemList.Select(x => new MaterialViewModel
                {
                    Name = x.Value.Item1.Name,
                    Cost = x.Value.Item2 * x.Value.Item1.Cost
                });
                orders.AddRange(elem);
            }
           // .Select(x => new MaterialViewModel
           // {
           //     Name = x.I,
           //     Cost = x.Cost,
           // })
           //.ToList();

            var materialsList = new List<ReportMaterialsViewModel>();

            materialsList = orders.GroupBy(x => x.Name).Select(x => new ReportMaterialsViewModel
            {
                MaterialName = x.Key,
                Sum = x.Sum(x => x.Cost)
            }).ToList();
            return materialsList;
        }

        public void SaveFurnituresToExelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelStoreKeeperInfo
            {
                Title = "Список мебели",
                FurnitureMaterials = GetFurnitureComponent(model.Ids)
            });
        }

        public void SaveFurnituresToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordStoreKeeperInfo
            {
                Title = "Список мебели",
                FurnitureMaterialsList = GetFurnitureComponent(model.Ids)
            });
        }

        public void SaveMaterialsToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfStoreKeeperInfo
            {
                Title = "Список материалов",
                DateFrom = model.DateFrom!.Value,
                DateTo = model.DateTo!.Value,
                Materials = GetOrders(model)
            });
        }
    }
}
