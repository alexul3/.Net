using FurnitureAssemblyDataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyDataModels.Models
{
    public class IOrderInfoModel
    {
        PaymentType PaymentType { get; }
        DateTime DateCreate { get; }
        string CustomerName { get; }
        double Sum { get; }
        int UserId { get; }
    }
}
