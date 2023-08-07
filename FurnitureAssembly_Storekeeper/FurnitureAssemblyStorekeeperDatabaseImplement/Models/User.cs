using FurnitureAssembly_Storekeeper_Contracts.BindingModels;
using FurnitureAssembly_Storekeeper_Contracts.ViewModels;
using FurnitureAssembly_Storekeeper_DataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyStorekeeperDatabaseImplement.Models
{
	public class User : IUser
	{
		public int Id { get; set; }
		[Required]
		public string Login { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;
		[Required]
		public string UserName { get; set; } = string.Empty;
		[Required]
		[ForeignKey("RoleId")]
		public int RoleId { get; set; }
		public virtual IRole Role { get; set; }
		
		[ForeignKey("FurnitureId")]
		public virtual List<Furniture> Furnitures { get; set; } = new();
		[ForeignKey("MaterialId")]
		public virtual List<Material> Materials { get; set; } = new();
		public static User? Create(UserBindingModel? model)
		{
			if (model == null)
			{
				return null;
			}
			return new User()
			{
				Id = model.Id,
				Login = model.Login,
				Password = model.Password,
				UserName = model.UserName,
				Role = model.Role
			};
		}
		public void Update(UserBindingModel? model)
		{
			if (model == null)
			{
				return;
			}
			Login = model.Login;
			Password = model.Password;
			UserName = model.UserName;
			Role = model.Role;
		}
		public UserViewModel GetViewModel => new()
		{
			Id = Id,
			Password = Password,
			Login = Login,
			UserName = UserName,
			Role = Role
		};
	}
}
