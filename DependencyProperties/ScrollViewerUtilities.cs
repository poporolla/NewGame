using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NewGame
{
	public class ScrollViewerUtilities
	{
		public static readonly DependencyProperty HorizontalOffsetProperty = DependencyProperty.RegisterAttached(
			"HorizontalOffset",
			typeof(double), 
			typeof(ScrollViewerUtilities), 
			new FrameworkPropertyMetadata(
				(double)0.0, 
				new PropertyChangedCallback(OnHorizontalOffsetChanged)
				)
			);

		public static double GetHorizontalOffset(DependencyObject d)
		{
			return (double) d.GetValue(HorizontalOffsetProperty);
		}
		
		public static void SetHorizontalOffset(DependencyObject d, double value)
		{
			d.SetValue(HorizontalOffsetProperty, value);
		}

		private static void OnHorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var viewer = (ScrollViewer)d;
			viewer.ScrollToHorizontalOffset((double) e.NewValue);
		}

		public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.RegisterAttached(
			"VerticalOffset", 
			typeof(double), 
			typeof(ScrollViewerUtilities),
			new FrameworkPropertyMetadata(
				(double)0.0,
				new PropertyChangedCallback(OnVerticalOffsetChanged)
				)
			);

		public static double GetVerticalOffset(DependencyObject d)
		{
			return (double) d.GetValue(VerticalOffsetProperty);
		}

		public static void SetVerticalOffset(DependencyObject d, double value)
		{
			d.SetValue(VerticalOffsetProperty, value);
		}

		private static void OnVerticalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var viewer = (ScrollViewer)d;
			viewer.ScrollToVerticalOffset((double) e.NewValue);
		}

		//public static readonly DependencyProperty ExtHeightProperty = DependencyProperty.RegisterAttached(
		//	"ExtHeight",
		//	typeof(double),
		//	typeof(ScrollViewerUtilities),
		//	new FrameworkPropertyMetadata(
		//		(double)0.0,
		//		new PropertyChangedCallback(OnVerticalOffsetChanged)
		//		)
		//	);

		//public static double GetVerticalOffset(DependencyObject d)
		//{
		//	return (double)d.GetValue(ExtHeightProperty);
		//}

		//public static void SetVerticalOffset(DependencyObject d, double value)
		//{
		//	d.SetValue(ExtHeightProperty, value);
		//}

		//private static void OnVerticalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		//{
		//	var viewer = (ScrollViewer)d;
		//	viewer.Ex((double)e.NewValue);
		//}

	}
}
