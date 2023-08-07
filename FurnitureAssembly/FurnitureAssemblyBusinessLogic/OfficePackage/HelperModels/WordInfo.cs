using FurnitureAssemblyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels
{
	public class WordInfo
	{
		public string FileName { get; set; } = "C:\\temp\\word_worker.docx";
		public string Title { get; set; } = string.Empty;
		public List<ReportSetFurnitureModuleWorkerViewModel> SetsFurnitureModules { get; set; } = new();
	}
}
