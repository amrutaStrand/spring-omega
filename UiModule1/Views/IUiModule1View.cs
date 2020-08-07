namespace Agilent.OpenLab.UiModule1
{
    /// <summary>
    /// IUiModule1View
    /// </summary>
    public interface IUiModule1View
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model. 
        /// </value>
        IUiModule1ViewModel Model { get; set; }

        #endregion
    }
}