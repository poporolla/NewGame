using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NewGame
{
	public class SaperViewModel : INotifyPropertyChanged
	{
		private int minesCount;
		private int fieldSize;
		private List<SaperCell> saperCells;
		private int width;
		private int height;
		private bool isGameStarted = false;
		private Window Window { get; set; }
		public SaperViewModel(Sector sector, Window dialogResult)
		{
			Window = dialogResult;
			switch (sector.Level)
			{
				case 0:
					MinesCount = 10;
					FieldSize = 10;
					break;
				case 1:
					MinesCount = 40;
					FieldSize = 20;
					break;
				case 2:
					MinesCount = 60;
					FieldSize = 30;
					break;
				default:
					MinesCount = 10;
					FieldSize = 0;
					break;
			}
			SaperCells = new List<SaperCell>();
			for (int i = 0; i < FieldSize; i++)
			{
				for (int z = 0; z < FieldSize; z++)
				{
					SaperCells.Add(new SaperCell(i, z));
				}
			}

			Width = FieldSize * 22 + 16;
			Height = FieldSize * 22 + 60;
			
		}

		RelayCommand cellClickCommand;
		public RelayCommand CellClickCommand
		{
			get
			{
				return cellClickCommand ??
					(cellClickCommand = new RelayCommand(obj =>
					{
						SaperCell cell = obj as SaperCell;
						(cell).IsActive = false;
						if (isGameStarted)
						{
							// DOPISAT' notification
						}
						else
						{
							isGameStarted = true;
							StartTheGame(cell);
						}
						NotificationCells(cell);
						int activeCells = 0;
						int activeMineCells = 0;
						foreach(SaperCell saperCell in SaperCells)
						{
							if(saperCell.IsActive && saperCell.FillType == "*")
							{
								activeMineCells++;
							}
							if (saperCell.IsActive)
							{
								activeCells++;
							}
						}
						if(MinesCount == activeMineCells && MinesCount == activeCells)
						{
							MessageBox.Show("Win!");
							Window.DialogResult = true;
						}
						if (MinesCount > activeMineCells)
						{
							MessageBox.Show("Loose!");
							Window.DialogResult = false;
						}
					}, obj =>
					{
						if (obj != null)
						{
							return (obj as SaperCell).IsActive;
						}
						else return false;
					}));
			}
		}
		RelayCommand flagCommand;
		public RelayCommand FlagCommand
		{
			get
			{
				return flagCommand ??
					(flagCommand = new RelayCommand(obj =>
					{
						if (!isGameStarted) return;
						MouseButtonEventArgs eventArgs = obj as MouseButtonEventArgs;
						
						Border brdr = eventArgs.Source as Border;
						//MessageBox.Show(brdr.BorderBrush.ToString());
						if(brdr.BorderBrush == System.Windows.Media.Brushes.Green)
						{
							brdr.BorderBrush = System.Windows.Media.Brushes.Gray;
							brdr.BorderThickness = new Thickness(1);
						}
						else
						{
							brdr.BorderBrush = System.Windows.Media.Brushes.Green;
							brdr.BorderThickness = new Thickness(4);
						}
						//btn.BorderBrush = System.Windows.Media.Brushes.Green;


						//SaperCell cell = obj as SaperCell;
						//(cell).IsActive = false;
						//if (isGameStarted)
						//{
						//	if(cell.IsActive)

						//}
						//else
						//{
						//	isGameStarted = true;
						//	StartTheGame(cell);
						//}
						//NotificationCells(cell);
					}));
			}
		}
		private void NotificationCells(SaperCell cell)
		{
			for(int x = cell.PosX - 1; x <= cell.PosX + 1; x++)
			{
				for (int y = cell.PosY - 1; y <= cell.PosY + 1; y++)
				{
					//проверяем, существует ли ячейка
					if (!SaperCells.Any((em) => em.PosX == x && em.PosY == y)) continue;
					
					SaperCell cellToNotif = SaperCells[SaperCells.FindIndex((em) => em.PosX == x && em.PosY == y)];
					if (cellToNotif.FillType == null && cellToNotif.IsActive == true)
					{
						cellToNotif.IsActive = false;
						NotificationCells(cellToNotif);
					}
					else if (cellToNotif.FillType == "*") continue;
					else
					{
						cellToNotif.IsActive = false;
					}
				}
			}
		}
		private void StartTheGame(SaperCell cell)
		{
			Random random = new Random();
			for (int i = 0; i < MinesCount;)
			{
				SaperCell newCell = new SaperCell(
					random.Next(0, FieldSize),
					random.Next(0, FieldSize)
					);
				if (newCell.PosX == cell.PosX && newCell.PosY == cell.PosY) continue;
				SaperCell oldCell = SaperCells[SaperCells.FindIndex((em) => em.PosX == newCell.PosX && em.PosY == newCell.PosY)];

				switch (oldCell.FillType)
				{
					case "*":
						continue;
					default:
						oldCell.FillType = "*";
						for (int x = oldCell.PosX - 1; x <= oldCell.PosX + 1; x++)
						{
							for (int y = oldCell.PosY - 1; y <= oldCell.PosY + 1; y++)
							{
								if (!SaperCells.Any((em) => em.PosX == x && em.PosY == y)) continue;
								SaperCell saperCell = SaperCells[SaperCells.FindIndex((em) => em.PosX == x && em.PosY == y)];
								if (saperCell.FillType!=null && saperCell.FillType!= "*")
								{
									saperCell.FillType = (int.Parse(saperCell.FillType) + 1).ToString();
								}
								else if (saperCell.FillType == null)
								{
									saperCell.FillType = "1";
								}
								else continue;
							}
						}
						i++;
						break;
				}
			}
		}

		public List<SaperCell> SaperCells
		{
			get
			{
				return saperCells;
			}
			set
			{
				saperCells = value;
				OnPropertyChanged("SaperCells");
			}
		}
		public int MinesCount
		{
			get
			{
				return minesCount;
			}
			set
			{
				minesCount = value;
				OnPropertyChanged("MinesCount");
			}
		}
		public int FieldSize
		{
			get
			{
				return fieldSize;
			}
			set
			{
				fieldSize = value;
				OnPropertyChanged("FieldSize");
			}
		}
		public int Width
		{
			get
			{
				return width;
			}
			set
			{
				width = value;
				OnPropertyChanged("Width");
			}
		}
		public int Height
		{
			get
			{
				return height;
			}
			set
			{
				height = value;
				OnPropertyChanged("Height");
			}
		}


		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string property = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
	}
}
