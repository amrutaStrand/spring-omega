using System;
using System.Windows;

namespace OmegaUIControls.OmegaUIUtils
{
	public class UIConstants
	{
		public const int PANEL_WIDTH = 192;
		public const int PANEL_HEIGHT = 20;

		// 10 extra for border
		public static readonly Size PANEL_PREFERRED_SIZE = new Size(PANEL_WIDTH + 10, PANEL_HEIGHT + 10);

		public const int LABEL_WIDTH = PANEL_WIDTH / 2;
		public const int LABEL_HEIGHT = PANEL_HEIGHT;
		public static readonly Size LABEL_PREFERRED_SIZE = new Size(LABEL_WIDTH, LABEL_HEIGHT);

		public const int TEXT_WIDTH = PANEL_WIDTH / 2;
		public const int TEXT_HEIGHT = PANEL_HEIGHT;
		public static readonly Size TEXT_PREFERRED_SIZE = new Size(TEXT_WIDTH, TEXT_HEIGHT);
	}
}
