using FurnitureAssemblyDataModels.Enums;
using FurnitureAssemblyDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.ViewModels
{
    public class OrderInfoViewModel : IOrderInfoModel
    {
        [DisplayName("Номер")]
        public int Id { get; set; }
        [DisplayName("Имя заказчика")]
        public string CustomerName { get; set; } = string.Empty;
        [DisplayName("Тип оплаты")]
        public PaymentType PaymentType { get; set; } = PaymentType.Неизвестен;
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        [DisplayName("Сумма")]
        public double Sum { get; set; }
        public int UserId { get; set; }
        [DisplayName("Менеджер")]
        public string UserName { get; set; } = string.Empty;
    }
}
