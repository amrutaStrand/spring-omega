namespace Agilent.OpenLab.UiModule1
{
    #region

    using System;
    using System.Windows.Media.Imaging;

    using Agilent.OpenLab.UiModule1.Properties;

    #endregion

    partial class UiModule1Module
    {
        #region Public Properties

        /// <summary>
        /// Gets the caption.
        /// </summary>
        public override string Caption
        {
            get
            {
                return Resources.UiModule1Caption;
            }
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        public override BitmapImage Image
        {
            get
            {
                return
                    new BitmapImage(
                        new Uri(
                            "pack://application:,,,/Agilent.OpenLab.UiModule1;component/Images/TestImage.png"));
            }
        }

        /// <summary>
        /// Gets the key tip.
        /// </summary>
        public override string KeyTip
        {
            get
            {
                return "M";
            }
        }

        /// <summary>
        /// Gets the tooltip.
        /// </summary>
        public override string Tooltip
        {
            get
            {
                return Resources.UiModule1Hint;
            }
        }

        #endregion
    }
}