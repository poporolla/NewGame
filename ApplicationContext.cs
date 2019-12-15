using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGame
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext()
			: base("DefaultConnection")
		{

		}
		public DbSet<Sector> Sectors { get; set; }
		public DbSet<ClearSector> ClearSectors { get; set; }
		public DbSet<MinedSector> MinedSectors { get; set; }
	}
}
