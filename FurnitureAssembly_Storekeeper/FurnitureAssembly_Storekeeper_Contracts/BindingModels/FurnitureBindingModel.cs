using FurnitureAssembly_Storekeeper_DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_Storekeeper_Contracts.BindingModels
{
    public class FurnitureBindingModel : IFurniture
    {
        public string Name { get; set; } = string.Empty;

        public double Cost {get; set; }   

        public DateTime DateCreate { get; set; } = DateTime.Now;

        public Dictionary<int, (IMaterial, int)> FurnitureMaterials { get; set; } = new();

        public IUser User { get; set;  } = new UserBindingModel();

        public int Id { get; set; }

    }
}
