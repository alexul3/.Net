using FurnitureAssemblyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels.StorekeeperModels
{
    public class PdfStoreKeeperInfo
    {
        public string FileName { get; set; } = "C:\\temp\\pdf_storekeeper.pdf";
        public string Title { get; set; } = string.Empty;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportMaterialsViewModel> Materials { get; set; }
    }
}
