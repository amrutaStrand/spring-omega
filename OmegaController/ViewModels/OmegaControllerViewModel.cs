namespace Agilent.OpenLab.OmegaController
{
    #region

    using Agilent.OpenLab.Framework.UI.Module;
    using ClassLibrary1;
    using Microsoft.Practices.Unity;

    #endregion

    public partial class OmegaControllerViewModel : BaseViewModel, IOmegaControllerViewModel
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "OmegaControllerViewModel" /> class.
        /// </summary>
        /// <param name = "container">The container.</param>
        public OmegaControllerViewModel(IUnityContainer container)
            : base(container)
        {
            this.InitializeCommands();
            this.SubscribeEvents();
            UIControlFactory.RegisterUIControls(container);
        }

        #endregion                    
    }
}