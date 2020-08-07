﻿namespace Agilent.OpenLab.OmegaController
{
    /// <summary>
    /// </summary>
    public partial class OmegaControllerViewModel
    {
        #region Methods

        /// <summary>
        ///   Called when [activated].
        /// </summary>
        /// <remarks>
        /// This method is called when the module gets activated. This is usually the case when the 
        /// module switches from hidden state to visible state. To optimize performance, modules 
        /// should not directly process events if they are hidden, but should only track those 
        /// events and handle them only if the module becomes visible again. The module should 
        /// track unprocessed events in the event handlers (if the module is not active) and should 
        /// process the events in this method if the module becomes active again.
        /// </remarks>
        protected override void OnActivated()
        {
        }

        /// <summary>
        ///   Called when [deactivated].
        /// </summary>
        protected override void OnDeactivated()
        {
        }

        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <remarks>
        /// Use this method to load user settings.
        /// </remarks>
        protected override void OnLoaded()
        {
            // Load user settings            
        }

        /// <summary>
        /// Called when [unload].
        /// </summary>
        /// <remarks>
        /// Use this method to save user settings. This is also the place to unsubscribe any
        /// subscribed events.
        /// </remarks>
        protected override void OnUnload()
        {
            // Save User Settings

            // Unsubscribe
            this.UnsubscribeEvents();
        }

        #endregion
    }
}