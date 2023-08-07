using FurnitureAssemblyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels.StorekeeperModels
{
    public class ExcelStoreKeeperInfo
    {
		public string FileName { get; set; } = "C:\\temp\\excel_storekeeper.xlsx";
		public string Title { get; set; } = string.Empty;
		public List<ReportFurnitureMaterialViewModel> FurnitureMaterials
		{
			get;
			set;
		} = new();
	}
}
