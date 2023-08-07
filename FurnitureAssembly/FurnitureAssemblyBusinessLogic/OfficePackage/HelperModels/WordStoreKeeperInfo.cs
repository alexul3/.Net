using FurnitureAssemblyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels.StorekeeperModels
{
    public class WordStoreKeeperInfo
    {
        public string FileName { get; set; } = "C:\\temp\\word_storekeeper.docx";
        public string Title { get; set; } = string.Empty;
        public List<ReportFurnitureMaterialViewModel> FurnitureMaterialsList { get; set; } = new();
    }
}
