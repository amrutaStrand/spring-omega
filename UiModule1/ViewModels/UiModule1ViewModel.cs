namespace Agilent.OpenLab.UiModule1
{
    #region

    using Agilent.OpenLab.Framework.UI.Module;
    using Agilent.OpenLab.Spring.Omega;
    using Microsoft.Practices.Unity;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

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
            UIInput input = new UIInput();
            //UIInput input2 = new UIInput();
            //input2.AddInput("Label", "Name");
            //input2.AddInput("Value", "Enter Name");
            Dictionary<string, UIElement> labels = new Dictionary<string, UIElement>();
            Label panel1 = new Label();
            panel1.Content = "Strand (Option A)";
            Label panel2 = new Label();
            panel2.Content = "Life (Option B)";
            Label panel3 = new Label();
            panel3.Content = "Sciences (Option C)";
            //IUIControl panel4 = container.Resolve<IUIControl>("String");
            //panel4.SetInput(input);
            labels.Add("Option A", panel1);
            labels.Add("Option B", panel2);
            labels.Add("Option C", panel3);
            //labels.Add("Option D", (UIElement)panel4);
            input.AddInput("Options", labels);
            //input.AddInput("Label", "Name");
            //input.AddInput("Value", "Enter Name");
            //UIControl = UIControlFactory.GetUIControl("String", parameters);

            UIControl = container.Resolve<IUIControl>("RadioPanel");
            UIControl.SetInput(input);
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
        public IUIControl UIControl { get; set; }

        #endregion
    }
}