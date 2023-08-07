using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDataModels.Enums;
using FurnitureAssemblyDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.BindingModels
{
	public class OrderBindingModel : IOrderModel
	{
		public int Id { get; set; }
		public int SetId { get; set; }
        public int OrderInfoId { get; set; }
        public int Count { get; set; }
	}
}
