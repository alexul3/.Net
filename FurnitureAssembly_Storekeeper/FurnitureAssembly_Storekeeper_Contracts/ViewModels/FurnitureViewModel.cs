using FurnitureAssembly_Storekeeper_DataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_Storekeeper_Contracts.ViewModels
{
    public class FurnitureViewModel : IFurniture
    {
        [DisplayName("Название изделия")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("Цена изделия")]
        public double Cost { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; } 

        public Dictionary<int, (IMaterial, int)> FurnitureMaterials { get; set; } = new ();
        [DisplayName("Изготовитель")]
        public IUser User { get; set; } = new UserViewModel();

        public int Id { get; set; }
    }
}
