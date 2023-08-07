using FurnitureAssembly_WorkerDataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerDataModels.Models
{
	public interface IOrderModel : IId
	{
		string CustomerName { get; }
		PaymentType PaymentType { get; }
		int SetId { get; }
		int UserId { get; }
		double Sum { get; }
		DateTime DateCreate { get; }
	}
}
