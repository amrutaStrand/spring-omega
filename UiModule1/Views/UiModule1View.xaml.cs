﻿namespace Agilent.OpenLab.UiModule1
{
    #region

    using System.Windows;
    using ClassLibrary1;

    #endregion

    /// <summary>
    /// Interaction logic for UiModule1View.xaml
    /// </summary>
    /// <remarks>
    /// </remarks>
    public partial class UiModule1View : IUiModule1View
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UiModule1View"/> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public UiModule1View()
        {
            this.InitializeComponent();
            //this.UIControlHost.Children.Add(new StringControl("Name", "Enter Name"));
            
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model. 
        /// </value>
        /// <remarks>
        /// </remarks>
        public IUiModule1ViewModel Model
        {
            get
            {
                return this.DataContext as IUiModule1ViewModel;
            }

            set
            {
                this.DataContext = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data. 
        /// </param>
        /// <remarks>
        /// </remarks>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.UIControlHost.Children.Add(Model.UIControl.GetUIElement());
        }

        /// <summary>
        /// Called when [unload].
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data. 
        /// </param>
        /// <remarks>
        /// </remarks>
        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
        }

        #endregion
    }
}