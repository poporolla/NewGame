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
using System.Windows.Media;

namespace NewGame
{
	public class MapControl : INotifyPropertyChanged
	{
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
		private double centerX = 24550;
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
		private double centerY = 24700;
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
						
						if(Mouse.LeftButton == MouseButtonState.Pressed)
						{
							CenterX += (MousePosX - point.X);
							CenterY += (MousePosY - point.Y);
						}
						else
						{
							MousePosX = point.X;
							MousePosY = point.Y;
						}
					}));
			}
		}
		private double width = 50000;
		public double Width
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

		RelayCommand scaleCommand;
		public RelayCommand ScaleCommand
		{
			get
			{
				return scaleCommand ??
					(scaleCommand = new RelayCommand(obj =>
					{
						MouseWheelEventArgs eventArgs = obj as MouseWheelEventArgs;
						eventArgs.Handled = true;
						Canvas canvas = eventArgs.Source as Canvas;

						if (ScaleX < 1 && eventArgs.Delta > 0)
						{
							ScaleX += 0.05;
							ScaleY += 0.05;
						}
						else if (ScaleX > 0.55 && eventArgs.Delta < 0)
						{
							ScaleX -= 0.05;
							ScaleY -= 0.05;
						}
						canvas.RenderTransform = new ScaleTransform(ScaleX, ScaleY, CenterX + 450, CenterY + 300);
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
