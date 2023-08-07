using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerDataModels.Models
{
	public interface ISetModel : IId
	{
		string Name { get; }
		double Cost { get; }
		DateTime DateCreate { get; }
		int UserId { get; }
		Dictionary<int, (IFurnitureModuleModel, int)> SetFurnitureModules { get; }
	}
}
