using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyBusinessLogic.BusinessLogics
{
    public interface IReportStorekeeperLogic
    {
        /// <summary>
        /// Получение списка мебели с разбиением по материалам
        /// </summary>
        /// <returns></returns>
        List<ReportFurnitureMaterialViewModel> GetFurnitureComponent(int[]? ids);

        /// <summary>
        /// Получение отчета по движению материалов за период времени
        /// </summary>
        /// <returns></returns>
        List<ReportMaterialsViewModel> GetOrders(ReportBindingModel model);

        /// <summary>
        /// Сохранение отчета по гарнитурам в файл-Word
        /// </summary>
        /// <param name="model"></param>
        void SaveFurnituresToWordFile(ReportBindingModel model);

        /// <summary>
        /// Сохранение отчета по гарнитурам в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        void SaveFurnituresToExelFile(ReportBindingModel model);

        /// <summary>
        /// Сохранение отчета о материалах в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        void SaveMaterialsToPdfFile(ReportBindingModel model);
    }
}
