﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyContracts.SearchModels
{
	public class MaterialSearchModel
	{
		public int? Id { get; set; }
		public string? Name { get; set; }
		public int? UserId { get; set; }
	}
}
