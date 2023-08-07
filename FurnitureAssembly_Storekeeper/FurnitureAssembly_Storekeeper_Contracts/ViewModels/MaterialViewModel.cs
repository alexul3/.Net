using FurnitureAssembly_Storekeeper_DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_Storekeeper_Contracts.ViewModels
{
    public class MaterialViewModel : IMaterial
    {
        public string Name { get; set; } = string.Empty;

        public double Cost { get; set; }

        public IScope Scope { get; set; } = new ScopeViewModel();

        public int Id { get; set; } 
    }
}
