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
		public ApplicationViewModel()
		{
			db = new ApplicationContext();
			db.Sectors.Load();
			Sectors = db.Sectors.Local.ToBindingList();

			MapControl = new MapControl();
		}

		RelayCommand addCommand;
		public RelayCommand AddCommand
		{
			get
			{
				return addCommand ??
					(addCommand = new RelayCommand(obj =>
					{
						StackPanel stackPanel = obj as StackPanel;
						Sector minenSector = new MinedSector();
						minenSector.PosX = int.Parse((stackPanel.Children[3] as TextBox).Text);
						minenSector.PosY = int.Parse((stackPanel.Children[4] as TextBox).Text);
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

		private void NewSector(Sector sector)
		{
			//int centerX = Math.Abs(Sectors.Min(em => em.PosX)) * 40 + 25000;
			//int centerY = Sectors.Max(em => em.PosY) * 40 + 25000;

			////int deltaX = Sectors.Min(em => em.PosX) - sector.PosX;
			////deltaX = (deltaX > 0) ? deltaX : 0;
			////int deltaY = sector.PosY - Sectors.Max(em => em.PosY);
			////deltaY = (deltaY > 0) ? deltaY : 0;

			////if (deltaX > 0 || deltaY > 0)
			////{
			////	foreach(Sector sec in Sectors)
			////	{
			////		Sector secDB = db.Sectors.Find(sec.Id);
			////		secDB.Left += deltaX * 40;
			////		secDB.Top += deltaY * 40;
			////		db.Entry(secDB).State = EntityState.Modified;
			////	}
			////}

			//sector.Left = centerX + (sector.PosX * 40);
			//sector.Top = centerY - (sector.PosY * 40);
			sector.Left = 25000 + (sector.PosX * 40);
			sector.Top = 25000 - (sector.PosY * 40);
			db.Sectors.Add(sector);
			db.SaveChanges();
		}

		// Controller for GameField
		public MapControl MapControl { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string property = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
	}
}
