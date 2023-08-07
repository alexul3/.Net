using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyDatabaseImplement.Models
{
	public class User : IUserModel
	{
		public int Id { get; set; }
		[Required]
		public string Login { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;
		[Required]
		public string Name { get; set; } = string.Empty;
		[Required]
		public int RoleId { get; set; }
		public virtual Role Role { get; set; }
		[ForeignKey("UserId")]
		public virtual List<Order> Orders { get; set; } = new();
		[ForeignKey("UserId")]
		public virtual List<Set> Sets { get; set; } = new();
		[ForeignKey("UserId")]
		public virtual List<FurnitureModule> FurnitureModules { get; set; } = new();
		[ForeignKey("UserId")]
		public virtual List<Furniture> Furnitures { get; set; } = new();
		[ForeignKey("UserId")]
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
				Name = model.Name,
				RoleId = model.RoleId
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
			Name = model.Name;
			RoleId = model.RoleId;
		}
		public UserViewModel GetViewModel => new()
		{
			Id = Id,
			Password = Password,
			Login = Login,
			Name = Name,
			RoleId = RoleId,
			RoleName = Role.Name
		};
	}
}
