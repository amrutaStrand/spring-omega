using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OmegaUIControls.OmegaUIUtils
{
	public class UIConstants
	{
		public const int ControlWidth = 192;
		public const int ControlHeight = 20;

		// 10 extra for border
		public static readonly Size ControlSize = new Size(ControlWidth + 10, ControlWidth + 10);

		public const int ControlMaxWidth = 1000;
		public const int ControlMaxHeight = 400;

		public const int ControlMinWidth = 50;
		public const int ControlMinHeight = 50;

		public static readonly Thickness ControlMargin = new Thickness(10);
		public static readonly HorizontalAlignment ControlHorizontalAlignment = HorizontalAlignment.Left;

		public static readonly Thickness BorderThicknessError = new Thickness(2);
		public static readonly Color ColorError = (Color)ColorConverter.ConvertFromString("#fc5236");
		public const double OpacityError = 0.15;

		public static Style GetErrorToolTipStyle()
        {
			Style ErrorToolTipStyle = new Style(typeof(ToolTip));
			ErrorToolTipStyle.Setters.Add(new Setter(ToolTip.BackgroundProperty, new SolidColorBrush(UIConstants.ColorError)));
			ErrorToolTipStyle.Setters.Add(new Setter(ToolTip.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"))));
			ErrorToolTipStyle.Setters.Add(new Setter(ToolTip.FontFamilyProperty, new FontFamily("Calibri")));
			ErrorToolTipStyle.Setters.Add(new Setter(ToolTip.FontWeightProperty, FontWeights.Regular));
			ErrorToolTipStyle.Setters.Add(new Setter(ToolTip.FontSizeProperty, 14d));
			return ErrorToolTipStyle;
		}

		public static SolidColorBrush GetTextBackgroundError()
        {
			SolidColorBrush errorBrush = new SolidColorBrush();
			errorBrush.Color = UIConstants.ColorError;
			errorBrush.Opacity = UIConstants.OpacityError;
			return errorBrush;
		}
	}
}
