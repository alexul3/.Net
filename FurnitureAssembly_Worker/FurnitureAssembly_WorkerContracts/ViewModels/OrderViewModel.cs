using FurnitureAssembly_WorkerDataModels.Enums;
using FurnitureAssembly_WorkerDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerContracts.ViewModels
{
	public class OrderViewModel : IOrderModel
	{
		[DisplayName("Номер")]
		public int Id { get; set; }
		[DisplayName("Имя заказчика")]
		public string CustomerName { get; set; } = string.Empty;
		[DisplayName("Тип оплаты")]
		public PaymentType PaymentType { get; set; } = PaymentType.Неизвестен;
		public int SetId { get; set; }
		[DisplayName("Гарнитур")]
		public string SetName { get; set; } = string.Empty;
		[DisplayName("Дата создания")]
		public DateTime DateCreate { get; set; }
		[DisplayName("Сумма")]
		public double Sum { get; set; }
		public int UserId { get; set; }
		[DisplayName("Менеджер")]
		public string UserName { get; set; } = string.Empty;
	}
}
