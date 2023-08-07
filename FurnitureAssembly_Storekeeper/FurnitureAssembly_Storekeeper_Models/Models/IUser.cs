using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_Storekeeper_DataModels.Models
{
    public interface IUser : IId
    {
        string Login { get;}
        string Password { get;}
        string UserName { get;}
        IRole Role { get;}
    }
}
