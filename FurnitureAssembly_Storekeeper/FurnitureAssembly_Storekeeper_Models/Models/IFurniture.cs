using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_Storekeeper_DataModels.Models
{
    public interface IFurniture : IId
    {
        string Name { get; }
        double Cost { get; }
        DateTime DateCreate { get; }
        Dictionary<int, (IMaterial, int)> FurnitureMaterials { get; }
        IUser User { get; }
    }
}
