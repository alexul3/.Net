using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.ViewModels
{
    public class ReportFurnitureMaterialViewModel
    {
        public string FurnitureName { get; set; } = string.Empty;
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Materials { get; set; } = new();
    }
}
