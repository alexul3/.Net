using FurnitureAssembly_WorkerContracts.BindingModels;
using FurnitureAssembly_WorkerContracts.ViewModels;
using FurnitureAssembly_WorkerDataModels.Enums;
using FurnitureAssembly_WorkerDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerDatabaseImplement.Models
{
	public class Order : IOrderModel
	{
		public int Id { get; set; }
		[Required]
		public string CustomerName { get; set; } = string.Empty;
		[Required]
		public PaymentType PaymentType { get; set; } = PaymentType.Неизвестен;
		[Required]
		public int SetId { get; set; }
		public virtual Set Set { get; set; }
		[Required]
		public DateTime DateCreate { get; set; }
		[Required]
		public double Sum { get; set; }
		[Required]
		public int UserId { get; set; }
		public virtual User User { get; set; }
		public static Order? Create(OrderBindingModel? model)
		{
			if (model == null)
			{
				return null;
			}
			return new Order()
			{
				Id = model.Id,
				CustomerName = model.CustomerName,
				PaymentType = model.PaymentType,
				SetId = model.SetId,
				Sum = model.Sum,
				DateCreate = model.DateCreate,
				UserId = model.UserId
			};
		}
		public void Update(OrderBindingModel? model)
		{
			if (model == null)
			{
				return;
			}
			CustomerName = model.CustomerName;
			PaymentType = model.PaymentType;
			SetId = model.SetId;
			UserId = model.UserId;
		}
		public OrderViewModel GetViewModel => new()
		{
			Id = Id,
			CustomerName = CustomerName,
			PaymentType = PaymentType,
			SetId = SetId,
			SetName = Set.Name,
			DateCreate = DateCreate,
			Sum = Sum,
			UserId = UserId,
			UserName = User.Name
		};
	}
}
