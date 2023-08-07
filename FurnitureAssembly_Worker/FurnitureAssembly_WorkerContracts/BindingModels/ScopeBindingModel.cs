using FurnitureAssembly_WorkerDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace FurnitureAssembly_WorkerContracts.BindingModels
{
	public class ScopeBindingModel : IScopeModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
	}
}
