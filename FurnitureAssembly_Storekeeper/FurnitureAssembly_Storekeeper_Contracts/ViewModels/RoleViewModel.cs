using FurnitureAssembly_Storekeeper_DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_Storekeeper_Contracts.ViewModels
{
    public class RoleViewModel : IRole
    {
        public string Name { get; set; } = string.Empty;

        public int Id { get; set; }
    }
}
