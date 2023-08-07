using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerDataModels.Models
{
	public interface IScopeModel : IId
	{
		string Name { get; }
	}
}
