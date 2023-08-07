using FurnitureAssemblyDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace FurnitureAssemblyContracts.BindingModels
{
	public class MaterialBindingModel : IMaterialModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public double Cost { get; set; }
		public int ScopeId { get; set; }
		public int UserId { get; set; }
	}
}
