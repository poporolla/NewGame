using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGame
{
	public class ClearSector : Sector
	{
		public ClearSector()
		{
			Level = 0;
			FillType = "/NewGame;component/Images/SectorImages/Clear.jpg";
		}
	}
}
