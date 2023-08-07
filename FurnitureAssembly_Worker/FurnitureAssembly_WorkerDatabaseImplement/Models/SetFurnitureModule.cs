using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerDatabaseImplement.Models
{
	public class SetFurnitureModule
	{
		public int Id { get; set; }
		[Required]
		public int SetId { get; set; }
		[Required]
		public int FurnitureModuleId { get; set; }
		[Required]
		public int Count { get; set; }
		public virtual Set Set { get; set; } = new();
		public virtual FurnitureModule FurnitureModule { get; set; } = new();
	}
}
