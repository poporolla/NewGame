using System;
using System.Collections.Generic;
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
	public class MapControl : INotifyPropertyChanged
	{
		//public MapControl(IEnumerable<Sector> sectors)
		//{

		//	Width = (sectors.Max(em => em.PosX) + Math.Abs(sectors.Min(em => em.PosX))) * 40 + 200;
		//	Height = (sectors.Max(em => em.PosY) + Math.Abs(sectors.Min(em => em.PosY))) * 40 + 200;
		//}
		private double scaleX = 1;
		public double ScaleX
		{
			get
			{
				return scaleX;
			}
			set
			{
				scaleX = value;
				OnPropertyChanged("ScaleX");
			}
		}
		private double scaleY = 1;
		public double ScaleY
		{
			get
			{
				return scaleY;
			}
			set
			{
				scaleY = value;
				OnPropertyChanged("ScaleY");
			}
		}
		private double centerX = 25000;
		public double CenterX
		{
			get
			{
				return centerX;
			}
			set
			{
				centerX = value;
				OnPropertyChanged("CenterX");
			}
		}
		private double centerY = 25000;
		public double CenterY
		{
			get
			{
				return centerY;
			}
			set
			{
				centerY = value;
				OnPropertyChanged("CenterY");
			}
		}
		private double mousePosX;
		public double MousePosX
		{
			get
			{
				return mousePosX;
			}
			set
			{
				mousePosX = value;
				OnPropertyChanged("MousePosX");
			}
		}
		private double mousePosY;
		public double MousePosY
		{
			get
			{
				return mousePosY;
			}
			set
			{
				mousePosY = value;
				OnPropertyChanged("MousePosY");
			}
		}
		RelayCommand mousePos;
		public RelayCommand MousePos
		{
			get
			{
				return mousePos ??
					(mousePos = new RelayCommand(obj =>
					{
						MouseEventArgs eventArgs = obj as MouseEventArgs;
						Canvas canvas = eventArgs.Source as Canvas;
						Point point = Mouse.GetPosition(canvas);
						MousePosX = point.X;
						MousePosY = point.Y;
					}));
			}
		}
		//private double width;
		//public double Width
		//{
		//	get
		//	{
		//		return width;
		//	}
		//	set
		//	{
		//		width = value;
		//		OnPropertyChanged("Width");
		//	}
		//}
		//private double height;
		//public double Height
		//{
		//	get
		//	{
		//		return height;
		//	}
		//	set
		//	{
		//		height = value;
		//		OnPropertyChanged("Height");
		//	}
		//}

		RelayCommand scaleCommand;
		public RelayCommand ScaleCommand
		{
			get
			{
				return scaleCommand ??
					(scaleCommand = new RelayCommand(obj =>
					{
						MouseWheelEventArgs eventArgs = obj as MouseWheelEventArgs;
						//if (Sectors.ElementAt(0).Height < 1 && eventArgs.Delta > 0)
						//{
						//	scaleX += 0.05;
						//	scaleY += 0.05;
						//}

						Canvas canvas = eventArgs.Source as Canvas;
						Point point = Mouse.GetPosition(canvas);

						if (ScaleX < 1 && eventArgs.Delta > 0)
						{
							ScaleX += 0.05;
							ScaleY += 0.05;
						}
						else if (ScaleX > 0.1 && eventArgs.Delta < 0)
						{
							ScaleX -= 0.05;
							ScaleY -= 0.05;
						}
						//MessageBox.Show($"{listBox.Height}");
						//CenterX = listBox.ActualWidth / 2;
						//CenterY = listBox.ActualHeight / 2;

						CenterX = MousePosX;
						CenterY = MousePosY;

						//CenterX = 25000;
						//CenterY = 25000;

						//CenterX = MousePosX;
						//CenterY = MousePosY;

						//canvas.RenderTransform = new ScaleTransform(scaleX, scaleY);
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
