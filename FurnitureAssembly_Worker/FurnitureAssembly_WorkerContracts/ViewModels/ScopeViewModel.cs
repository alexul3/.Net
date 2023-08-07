using FurnitureAssembly_WorkerDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerContracts.ViewModels
{
	public class ScopeViewModel : IScopeModel
	{
		public int Id { get; set; }
		[DisplayName("Название области применения")]
		public string Name { get; set; } = string.Empty;
	}
}
