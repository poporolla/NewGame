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
		public SaperWindow(Sector sector)
		{
			InitializeComponent();

			this.DataContext = new SaperViewModel(sector, this);
		}
		//private void Button_Click(object sender, RoutedEventArgs e)
		//{
		//	this.DialogResult = true;
		//}
	}
}
