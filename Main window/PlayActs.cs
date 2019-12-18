using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NewGame
{
	public class PlayActs : INotifyPropertyChanged
	{
		public PlayActs(Sector sector, ApplicationViewModel applicationViewModel)
		{
			this.Sector = sector;
			this.MainVM = applicationViewModel;
		}
		public string Position { get => $"{Sector?.PosX} , {Sector?.PosY}"; }
		public int Level { get => Sector == null ? 0 : Sector.Level; }
		public string Type { get => Sector.GetSectorType(); }
		public string Description { get => Sector.GetDescription(); }
		public string SectorMeta { get => Sector.GetSectorMeta(); }
		public string FirstButton
		{
			get
			{
				string result = "";
				switch (Sector)
				{
					case ClearSector sect:
						result = "Построить";
						break;
					case MinedSector sect:
						result = "Разминировать";
						break;
					default:
						result = "Не определено";
						break;
				}
				return result;
			}
		}
		public RelayCommand FirstButtonCommand
		{
			get
			{
				switch (Sector)
				{
					case ClearSector sect:
						return CheckCommand;
					case MinedSector sect:
						return CheckCommand;
					default:
						return CheckCommand;
				}
			}
		}
		RelayCommand checkCommand;
		public RelayCommand CheckCommand
		{
			get
			{
				return checkCommand ??
					(checkCommand = new RelayCommand(obj =>
					{
						System.Windows.MessageBox.Show("Hi");
						bool result = MainVM.db.Sectors.Any((elem) => elem.PosX == 1 && elem.PosY == 0);
						System.Windows.MessageBox.Show(result.ToString());
					}));
			}
		}


		private Sector sector;
		public Sector Sector
		{
			get
			{
				return sector;
			}
			set
			{
				sector = value;
				OnPropertyChanged("Sector");
			}
		}
		private ApplicationViewModel MainVM { get; set; }


		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string property = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
	}
}
