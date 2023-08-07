using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.ViewModels
{
    public class ReportMaterialsViewModel
    {
        public int Id { get; set; }
        public string MaterialName { get; set; } = string.Empty;
        public double Sum { get; set; }
    }
}
