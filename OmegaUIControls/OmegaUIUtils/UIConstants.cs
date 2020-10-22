using System;
using System.Windows;
using System.Windows.Media;

namespace OmegaUIControls.OmegaUIUtils
{
	public class UIConstants
	{
		public const int ControlWidth = 192;
		public const int ControlHeight = 20;

		// 10 extra for border
		public static readonly Size ControlSize = new Size(ControlWidth + 10, ControlWidth + 10);

		public static readonly Thickness ControlMargin = new Thickness(10);
		public static readonly HorizontalAlignment ControlHorizontalAlignment = HorizontalAlignment.Left;

		public static readonly Thickness BorderThicknessError = new Thickness(2);
		public static readonly Color ColorError = (Color)ColorConverter.ConvertFromString("#fc5236");
		public const double OpacityError = 0.15;
	}
}
