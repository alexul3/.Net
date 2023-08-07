using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_Storekeeper_DataModels.Models
{
    public interface IMaterial : IId
    {
        string Name { get; }
        double Cost { get; }
        IScope Scope { get; }
    }
}
