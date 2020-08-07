namespace Agilent.OpenLab.UiModule1
{
    #region

    using Agilent.OpenLab.Framework.UI.Module;

    using Microsoft.Practices.Unity;
    using ClassLibrary1;
    using System.Windows;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// UiModule1ViewModel
    /// </summary>
    /// <remarks>
    /// </remarks>
    public partial class UiModule1ViewModel : BaseViewModel, IUiModule1ViewModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UiModule1ViewModel"/> class.
        /// </summary>
        /// <param name="container">
        /// The container. 
        /// </param>
        /// <remarks>
        /// </remarks>
        public UiModule1ViewModel(IUnityContainer container)
            : base(container)
        {
            this.View = this.UnityContainer.Resolve<IUiModule1View>();
            this.View.Model = this;
            this.SubscribeEvents();
            this.InitializeCommands();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Label", "Name");
            parameters.Add("Value", "Enter Name");
            UIControl = UIControlFactory.GetUIControl("String", parameters);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>
        /// The view. 
        /// </value>
        /// <remarks>
        /// </remarks>
        public IUiModule1View View { get; set; }

        /// <summary>
        /// Test UI Control
        /// </summary>
        public UIElement UIControl { get; set; }

        #endregion
    }
}