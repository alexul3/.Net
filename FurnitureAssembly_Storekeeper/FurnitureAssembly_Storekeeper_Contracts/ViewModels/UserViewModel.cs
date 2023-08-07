using FurnitureAssembly_Storekeeper_DataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_Storekeeper_Contracts.ViewModels
{
    public class UserViewModel : IUser
    {
        public int Id { get; set; }
        [DisplayName("Пароль")]
        public string Password { get; set; } = string.Empty;
        [DisplayName("Логин (эл. почта)")]
        public string Login { get; set; } = string.Empty;
        [DisplayName("ФИО клиента")]
        public string UserName { get; set; } = string.Empty;
        [DisplayName("Роль пользоватеоя")]
        public IRole Role { get; set; } = new RoleViewModel();
    }
}
