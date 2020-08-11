namespace Agilent.OpenLab.OmegaController
{
    #region

    using Agilent.OpenLab.Framework.UI.Module;
    using Agilent.OpenLab.Spring.Omega;
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
            UIControlRegistry.RegisterUIControls(container);
        }

        #endregion                    
    }
}