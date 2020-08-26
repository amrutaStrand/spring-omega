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

        //private IUnityContainer _container;

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

            CreateRadioCardControl();

        }

        private void CreateStringControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Label", "Name");
            input.AddInput("Value", "Enter Name");
            UIControl = this.UnityContainer.Resolve<IUIControl>("String");
            UIControl.SetInput(input);
        }

        private void CreateIntControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Label", "Name");
            input.AddInput("Value", 10);
            UIControl = this.UnityContainer.Resolve<IUIControl>("Int");
            UIControl.SetInput(input);
        }

        private void CreateFloatControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Label", "Name");
            input.AddInput("Value", 10f);
            UIControl = this.UnityContainer.Resolve<IUIControl>("Float");
            UIControl.SetInput(input);
        }

        private void CreateRadioCardControl()
        {
            Dictionary<string, object> labels = new Dictionary<string, object>();
            Label panel1 = new Label();
            panel1.Content = "Strand (Option A)";
            Label panel2 = new Label();
            panel2.Content = "Life (Option B)";
            Label panel3 = new Label();
            panel3.Content = "Sciences (Option C)";

            UIInput input2 = new UIInput();
            input2.AddInput("Label", "Name");
            input2.AddInput("Value", "Enter Name");
            IUIControl panel4 = this.UnityContainer.Resolve<IUIControl>("String");
            panel4.SetInput(input2);

            UIInput input3 = new UIInput();
            input3.AddInput("Description", "This is a Boolean Control");
            IUIControl panel5 = this.UnityContainer.Resolve<IUIControl>("Boolean");
            panel5.SetInput(input3);

            labels.Add("Option A", panel1);
            labels.Add("Option B", panel2);
            labels.Add("Option C", panel3);
            labels.Add("String Control", panel4);
            labels.Add("Boolean Control", panel5);
            UIInput input = new UIInput();
            input.AddInput("Options", labels);

            UIControl = this.UnityContainer.Resolve<IUIControl>("RadioCard");
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
