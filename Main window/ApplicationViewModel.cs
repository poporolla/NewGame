using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NewGame
{
	public class ApplicationViewModel : INotifyPropertyChanged
	{
		ApplicationContext db;
		IEnumerable<Sector> sectors;
		public IEnumerable<Sector> Sectors
		{
			get
			{
				return sectors;
			}
			set
			{
				sectors = value;
				OnPropertyChanged("Sector");
			}
		}
		private Sector selectedSector;
		public Sector SelectedSector
		{
			get
			{
				return selectedSector;
			}
			set
			{
				selectedSector = value;
				InicializeSectorInfo(value);
				OnPropertyChanged("SelectedSector");
			}
		}
		public ApplicationViewModel()
		{
			db = new ApplicationContext();
			db.Sectors.Load();
			Sectors = db.Sectors.Local.ToBindingList();

			MapControl = new MapControl();
		}

		RelayCommand checkCommand;
		public RelayCommand CheckCommand
		{
			get
			{
				return checkCommand ??
					(checkCommand = new RelayCommand(obj =>
					{
						bool result = db.Sectors.Any((elem) => elem.PosX == 1 && elem.PosY == 0);
						MessageBox.Show(result.ToString());
					}, obj =>
					{
						return !(obj == null);
					}));
			}
		}

		RelayCommand addCommand;
		public RelayCommand AddCommand
		{
			get
			{
				return addCommand ??
					(addCommand = new RelayCommand(obj =>
					{
						//StackPanel stackPanel = obj as StackPanel;
						//Sector minenSector = new MinedSector();
						//minenSector.PosX = int.Parse((stackPanel.Children[3] as TextBox).Text);
						//minenSector.PosY = int.Parse((stackPanel.Children[4] as TextBox).Text);
						Sector minenSector = new MinedSector() { PosX = 0, PosY = 0 };
						NewSector(minenSector);
					}));
			}
		}
		RelayCommand deleteCommand;
		public RelayCommand DeleteCommand
		{
			get
			{
				return deleteCommand ??
					(deleteCommand = new RelayCommand(obj =>
					{
						Sector sector = obj as Sector;
						db.Sectors.Remove(sector);
						db.SaveChanges();
					}, obj => 
					{
						if(obj == null)
						{
							return false;
						}
						else
						{
							Sector sector = obj as Sector;
							return !(sector.PosX == 0 && sector.PosY == 0);
						}
					}));
			}
		}
		/// <summary>
		/// Создание нового сектора
		/// </summary>
		/// <param name="sector"></param>
		private void NewSector(Sector sector)
		{
			if (db.Sectors.Any((elem) => elem.PosX == sector.PosX && elem.PosY == sector.PosY)) return;
			sector.Left = 25000 + (sector.PosX * 40);
			sector.Top = 25000 - (sector.PosY * 40);
			db.Sectors.Add(sector);
			db.SaveChanges();
		}
		/// <summary>
		/// Обновление информации о выбранном секторе
		/// </summary>
		/// <param name="sector"></param>
		private void InicializeSectorInfo(Sector sector)
		{
			Position = sector == null ? null : $"{sector.PosX} , {sector.PosY}";
			Level = sector == null ? null : sector.Level.ToString();
			Type = sector?.GetSectorType();
			Description = sector?.GetDescription();
			SectorMeta = sector?.GetSectorMeta();
			switch (sector)
			{
				case ClearSector sect:
					FirstButton = "Построить";
					FirstButtonCommand = CheckCommand;
					break;
				case MinedSector sect:
					FirstButton = "Разминировать";
					FirstButtonCommand = DeminingCommand;
					break;
				default:
					FirstButton = "Не определено";
					FirstButtonCommand = CheckCommand;
					break;
			}
		}

		/// <summary>
		/// Controller for gamefield
		/// </summary>
		public MapControl MapControl { get; set; }
		
		private string position;
		public string Position
		{
			get
			{
				return position;
			}
			set
			{
				position = value;
				OnPropertyChanged("Position");
			}
		}

		
		private string level;
		public string Level
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
		private string type;
		public string Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
				OnPropertyChanged("Type");
			}
		}
		private string description;
		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				description = value;
				OnPropertyChanged("Description");
			}
		}
		private string sectorMeta;
		public string SectorMeta
		{
			get
			{
				return sectorMeta;
			}
			set
			{
				sectorMeta = value;
				OnPropertyChanged("SectorMeta");
			}
		}
		private string firstButton;
		public string FirstButton
		{
			get
			{
				return firstButton;
			}
			set
			{
				firstButton = value;
				OnPropertyChanged("FirstButton");
			}
		}
		private RelayCommand firstButtonCommand;
		public RelayCommand FirstButtonCommand
		{
			get
			{
				return firstButtonCommand;
			}
			set
			{
				firstButtonCommand = value;
				OnPropertyChanged("FirstButtonCommand");
			}
		}
		RelayCommand deminingCommand;
		public RelayCommand DeminingCommand
		{
			get
			{
				return deminingCommand ??
					(deminingCommand = new RelayCommand(obj =>
					{

						SaperWindow saperWindow = new SaperWindow(SelectedSector);
						if (saperWindow.ShowDialog() == true)
						{
							for (int i = SelectedSector.PosX - 1; i <= SelectedSector.PosX + 1; i++)
							{
								for (int z = SelectedSector.PosY - 1; z <= SelectedSector.PosY + 1; z++)
								{
									if (i == SelectedSector.PosX && z == SelectedSector.PosY) continue;
									NewSector(new MinedSector() { PosX = i, PosY = z });
								}
							}

							Sector ClearSector = new ClearSector()
							{
								PosX = SelectedSector.PosX,
								PosY = SelectedSector.PosY,
								Top = SelectedSector.Top,
								Left = SelectedSector.Left
							};

							db.Sectors.Remove(SelectedSector);
							//SelectedSector = ClearSector;
							db.Sectors.Add(ClearSector);
							db.SaveChanges();

						}

					}, obj =>
					{
						return SelectedSector is MinedSector;
					}));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string property = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
	}
}
