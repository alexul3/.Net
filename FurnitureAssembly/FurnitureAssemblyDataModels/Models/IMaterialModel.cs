using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace FurnitureAssemblyDataModels.Models
{
	public interface IMaterialModel : IId
	{
		string Name { get; }
		double Cost { get; }
		int ScopeId { get; }
		int UserId { get; }
	}
}
