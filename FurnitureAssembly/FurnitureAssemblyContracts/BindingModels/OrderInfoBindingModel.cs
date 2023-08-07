using FurnitureAssemblyDataModels.Enums;
using FurnitureAssemblyDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.BindingModels
{
    public class OrderInfoBindingModel : IOrderInfoModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public PaymentType PaymentType { get; set; } = PaymentType.Неизвестен;
        public DateTime DateCreate { get; set; }
        public double Sum { get; set; }
        public int UserId { get; set; }
    }
}
