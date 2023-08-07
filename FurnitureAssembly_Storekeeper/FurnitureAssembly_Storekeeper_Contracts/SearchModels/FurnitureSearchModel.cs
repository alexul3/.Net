using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_Storekeeper_Contracts.SearchModels
{
    public class FurnitureSearchModel
    {
        public int? Id { get; set; }
        public string? FurnitureName { get; set; }
        public int? UserId { get; set; }
    }
}
