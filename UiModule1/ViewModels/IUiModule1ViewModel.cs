namespace Agilent.OpenLab.UiModule1
{
    #region

    using Agilent.OpenLab.Framework.UI.Common.Commands;
    using Agilent.OpenLab.Framework.UI.Layout.ModuleInterfaces;
    using Agilent.MHDA.Omega;

    #endregion

    /// <summary>
    /// IUiModule1ViewModel
    /// </summary>
    public interface IUiModule1ViewModel : IBaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Gets the toggle command A.
        /// </summary>
        /// <remarks>
        /// </remarks>
        ToggleCommand<object> ToggleCommandA { get; }

        /// <summary>
        /// Gets the trigger command B.
        /// </summary>
        /// <remarks>
        /// </remarks>
        TriggerCommand<object> TriggerCommandB { get; }

        /// <summary>
        /// Gets the view.
        /// </summary>
        IUiModule1View View { get; }

        /// <summary>
        /// 
        /// </summary>
        IUIControl UIControl { get; }

        #endregion
    }
}