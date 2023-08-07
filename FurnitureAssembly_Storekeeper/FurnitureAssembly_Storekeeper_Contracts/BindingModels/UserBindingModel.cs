using FurnitureAssembly_Storekeeper_DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_Storekeeper_Contracts.BindingModels
{
    public class UserBindingModel : IUser
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public IRole Role { get; set; } = new RoleBindingModel();
    }
}
