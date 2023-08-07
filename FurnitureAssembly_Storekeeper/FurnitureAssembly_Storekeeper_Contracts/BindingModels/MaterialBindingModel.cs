using FurnitureAssembly_Storekeeper_DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_Storekeeper_Contracts.BindingModels
{
    public class MaterialBindingModel : IMaterial
    {
        public string Name { get; set; } = string.Empty;

        public double Cost { get; set; }

        public IScope Scope { get; set; } = new ScopeBindingModel();

        public int Id { get; set; }
    }
}
