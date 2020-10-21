using System;
using System.Windows;

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
	}
}
