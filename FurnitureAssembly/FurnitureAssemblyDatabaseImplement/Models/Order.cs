using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDataModels.Enums;
using FurnitureAssemblyDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyDatabaseImplement.Models
{
	public class Order : IOrderModel
	{
		public int Id { get; set; }
		[Required]
		public int SetId { get; set; }
		public virtual Set Set { get; set; }
		[Required]
		public int Count { get; set; }
		[Required]
        public int OrderInfoId { get; set; }
        public virtual OrderInfo OrderInfo { get; set; }
        public static Order? Create(OrderBindingModel? model)
		{
			if (model == null)
			{
				return null;
			}
			return new Order()
			{
				Id = model.Id,
				SetId = model.SetId,
				Count = model.Count,
				OrderInfoId = model.OrderInfoId
			};
		}
		public void Update(OrderBindingModel? model)
		{
			if (model == null)
			{
				return;
			}
			SetId = model.SetId;
			Count = model.Count;
			OrderInfoId = model.OrderInfoId;
        }
		public OrderViewModel GetViewModel => new()
		{
			Id = Id,
			SetId = SetId,
			SetName = Set.Name,
			Count = Count,
            OrderInfoId = OrderInfoId,
			CustomerName = OrderInfo.CustomerName,
			PaymentType = OrderInfo.PaymentType,
			DateCreate = OrderInfo.DateCreate,
			Sum = OrderInfo.Sum,
			UserId = OrderInfo.UserId
		};
        
    }
}
