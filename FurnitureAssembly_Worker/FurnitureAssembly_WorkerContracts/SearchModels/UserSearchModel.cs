﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerContracts.SearchModels
{
	public class UserSearchModel
	{
		public int? Id { get; set; }
		public int? RoleId { get; set; }
		public string? Login { get; set; }
		public string? Name { get; set; }
		public string? Password { get; set; }
	}
}
