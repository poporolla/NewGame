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
		public ApplicationContext db;
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
				Acts = new PlayActs(value, this);
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
		/// Controller for gamefield
		/// </summary>
		public MapControl MapControl { get; set; }
		/// <summary>
		/// Controller for player actions
		/// </summary>
		private PlayActs acts;
		public PlayActs Acts
		{
			get
			{
				return acts;
			}
			set
			{
				acts = value;
				OnPropertyChanged("Acts");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string property = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
	}
}
