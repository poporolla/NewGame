using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGame
{
	public class ClearSector : Sector
	{
		public override string GetSectorType()
		{
			return "Пустой сектор";
		}
		public override string GetDescription()
		{
			return "Очищенная от мин земля.\n " +
				"Здесь вы можете строить необходимые постройки";
		}
		public override string GetSectorMeta()
		{
			return "Чтобы начать строительство, нажмите соответствующую кнопку";
		}
		public ClearSector()
		{
			Level = 0;
			FillType = "/NewGame;component/Images/SectorImages/Clear.jpg";
		}
	}
}
