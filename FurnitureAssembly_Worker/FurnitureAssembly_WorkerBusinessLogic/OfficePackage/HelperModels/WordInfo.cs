using FurnitureAssembly_WorkerContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerBusinessLogic.OfficePackage.HelperModels
{
	public class WordInfo
	{
		public string FileName { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public List<SetViewModel> Sets { get; set; } = new();
	}
}
