using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDataModels.Enums;
using FurnitureAssemblyDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyDatabaseImplement.Models
{
    public class OrderInfo : IOrderInfoModel
    {
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; } = string.Empty;
        [Required]
        public PaymentType PaymentType { get; set; } = PaymentType.Неизвестен;
        [Required]
        public DateTime DateCreate { get; set; }
        [Required]
        public double Sum { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public static OrderInfo? Create(OrderInfoBindingModel? model)
        {
            if (model == null)
            {
                return null;
            }
            return new OrderInfo()
            {
                Id = model.Id,
                CustomerName = model.CustomerName,
                PaymentType = model.PaymentType,
                Sum = 0,
                DateCreate = DateTime.Now.ToUniversalTime(),
                UserId = model.UserId
            };
        }
        public void Update(OrderInfoBindingModel? model)
        {
            if (model == null)
            {
                return;
            }
            CustomerName = model.CustomerName;
            PaymentType = model.PaymentType;
            DateCreate = model.DateCreate.ToUniversalTime();
            UserId = model.UserId;
            Sum = model.Sum;
        }
        public OrderInfoViewModel GetViewModel => new()
        {
            Id = Id,
            CustomerName = CustomerName,
            PaymentType = PaymentType,
            DateCreate = DateCreate,
            Sum = Sum,
            UserId = UserId,
            UserName = User.Name
        };
    }
}
