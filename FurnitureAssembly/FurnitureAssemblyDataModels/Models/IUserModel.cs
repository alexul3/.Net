using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyDataModels.Models
{
	public interface IUserModel : IId
	{
		string Login { get; }
		string Password { get; }
		string Name { get; }
		int RoleId { get; }
	}
}
