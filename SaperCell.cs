using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NewGame
{
	public class SaperCell : INotifyPropertyChanged
	{

		public SaperCell(int posX, int posY)
		{
			this.PosX = posX;
			this.PosY = posY;
		}



		private int posX;
		private int posY;
		private string fillType = null;
		private bool isActive = true;



		public int PosX
		{
			get
			{
				return posX;
			}
			set
			{
				posX = value;
				OnPropertyChanged("PosX");
			}
		}
		public int PosY
		{
			get
			{
				return posY;
			}
			set
			{
				posY = value;
				OnPropertyChanged("PosY");
			}
		}
		public string FillType
		{
			get
			{
				return fillType;
			}
			set
			{
				fillType = value;
				OnPropertyChanged("FillType");
			}
		}
		public string Text
		{
			get
			{
				return IsActive ? null : fillType;
			}
		}
		public bool IsActive
		{
			get
			{
				return isActive;
			}
			set
			{
				isActive = value;
				OnPropertyChanged("Text");
				OnPropertyChanged("IsActive");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string property = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
	}
}
