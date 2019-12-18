using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NewGame
{
	/// <summary>
	/// Логика взаимодействия для SaperWindow.xaml
	/// </summary>
	public partial class SaperWindow : Window
	{
		public Sector Sector { get; set; }
		//public ClearSector ClearSector { get; set; }
		public SaperWindow(Sector sector)
		{
			InitializeComponent();
			Sector = sector;
			//this.DataContext = MinedSector;

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}
	}
}
