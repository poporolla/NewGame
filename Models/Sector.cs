using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NewGame
{
	public abstract class Sector : INotifyPropertyChanged
	{
		public abstract string GetDescription();
		public abstract string GetSectorType();
		public abstract string GetSectorMeta();

		private int top;
		private int left;

		private int posX;
		private int posY;
		private string fillType;
		private int level;

		public int Id { get; set; }

		public int Top
		{
			get
			{
				return top;
			}
			set
			{
				top = value;
				OnPropertyChanged("Top");
			}
		}
		public int Left
		{
			get
			{
				return left;
			}
			set
			{
				left = value;
				OnPropertyChanged("Left");
			}
		}
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
		public int Level
		{
			get
			{
				return level;
			}
			set
			{
				level = value;
				OnPropertyChanged("Level");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string property = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
	}
}
