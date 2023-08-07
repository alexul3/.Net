using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyDataModels.Models
{
	public interface IFurnitureModel : IId
	{
		string Name { get; }
		double Cost { get; }
		DateTime DateCreate { get; }
        int UserId { get; }
        Dictionary<int, (IMaterialModel, int)> FurnitureMaterials { get; }
	}
}
