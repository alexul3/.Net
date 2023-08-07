using FurnitureAssemblyStorekeeperDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyStorekeeperDatabaseImplement
{
	public class FurnitureAssemblyDatabase : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (optionsBuilder.IsConfigured == false)
			{
				optionsBuilder.UseNpgsql("Host=zyzf.space;Port=5434;Database=kursach;Username=postgres;Password=kursach");
			}
			base.OnConfiguring(optionsBuilder);
		}
		public virtual DbSet<Furniture> Furnitures { get; set; }
		public virtual DbSet<FurnitureMaterial> FurnitureMaterials { get; set; }
		public virtual DbSet<Material> Materials { get; set; }
		public virtual DbSet<Role> Roles { get; set; }
		public virtual DbSet<Scope> Scopes { get; set; }
		public virtual DbSet<User> Users { get; set; }
	}
}
