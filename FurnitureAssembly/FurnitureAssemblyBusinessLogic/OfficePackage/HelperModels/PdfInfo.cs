using FurnitureAssemblyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels
{
	public class PdfInfo
	{
		public string FileName { get; set; } = "C:\\temp\\pdf_worker.pdf";
		public string Title { get; set; } = string.Empty;
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }
		public Tuple<List<ReportOrdersWorkerViewModel>, double> Orders { get; set; }
	}
}
