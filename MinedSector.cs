using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NewGame
{
	public class MinedSector : Sector
	{
		public MinedSector()
		{
			Level = 0;
			FillType = "/NewGame;component/Images/SectorImages/Minen.jpg";
		}
	}
}
