using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerDatabaseImplement.Models
{
	public class FurnitureMaterial
	{
		public int Id { get; set; }
		[Required]
		public int FurnitureId { get; set; }
		[Required]
		public int MaterialId { get; set; }
		[Required]
		public int Count { get; set; }
		public virtual Furniture Furniture { get; set; } = new();
		public virtual Material Material { get; set; } = new();
	}
}
