using FurnitureAssemblyDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.ViewModels
{
	public class UserViewModel : IUserModel
	{
		public int Id { get; set; }
		[DisplayName("Пароль")]
		public string Password { get; set; } = string.Empty;
		[DisplayName("Логин (эл. почта)")]
		public string Login { get; set; } = string.Empty;
		[DisplayName("ФИО пользователя")]
		public string Name { get; set; } = string.Empty;
		public int RoleId { get; set; }
		[DisplayName("Роль пользователя")]
		public string RoleName { get; set; } = string.Empty;
	}
}
