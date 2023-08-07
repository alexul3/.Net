using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyDataModels.Models
{
	public interface IRoleModel : IId
	{
		string Name { get; }
	}
}
