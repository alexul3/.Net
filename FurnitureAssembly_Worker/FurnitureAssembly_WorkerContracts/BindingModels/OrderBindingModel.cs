using FurnitureAssembly_WorkerContracts.ViewModels;
using FurnitureAssembly_WorkerDataModels.Enums;
using FurnitureAssembly_WorkerDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerContracts.BindingModels
{
	public class OrderBindingModel : IOrderModel
	{
		public int Id { get; set; }
		public string CustomerName { get; set; } = string.Empty;
		public PaymentType PaymentType { get; set; } = PaymentType.Неизвестен;
		public int SetId { get; set; }
		public DateTime DateCreate { get; set; }
		public double Sum { get; set; }
		public int UserId { get; set; }
	}
}
