using FurnitureAssemblyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels
{
	public class ExcelInfo
	{
		public string FileName { get; set; } = "C:\\temp\\excel_worker.xlsx";
		public string Title { get; set; } = string.Empty;
		public List<ReportSetFurnitureModuleWorkerViewModel> SetFurnitureModules
		{
			get;
			set;
		} = new();
	}
}
