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
		public override string GetSectorType()
		{
			return "Минное поле";
		}
		public override string GetDescription()
		{
			string result = "";
			switch (Level)
			{
				case (0):
					result = "Здесь минное поле, как и повсюду в этой местности";
					break;
				case (1):
					result = "Эта поверхность была не сильно задета боевыми действиями, разминирование не должно представлять сложности"; 
					break;
				case (2):
					result = "Рядом с этим местом был бой, так что при разминировании нужно проявить внимательность";
					break;
				default:
					result = "";
					break;
			}
			return result;
		}
		public override string GetSectorMeta()
		{
			return "Чтобы начать разминирование, нажмите соответствующую кнопку";
		}
		public MinedSector()
		{
			Random random = new Random();
			Level = random.Next(0, 3);
			FillType = "/NewGame;component/Images/SectorImages/Minen.jpg";
		}
	}
}
