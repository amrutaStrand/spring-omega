﻿namespace Agilent.OpenLab.UiModule1
{
    #region

    using Agilent.OpenLab.Framework.UI.Module;
    using Agilent.OpenLab.Spring.Omega;
    using global::OpenLab.Agilent.Spring.Algorithm;
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

            UIControl = CreateRadioCardControl();

        }

        private IUIControl CreateStringControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Label", "Name");
            input.AddInput("Value", "Enter Name");
            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("String");
            panel.SetInput(input);
            return panel;
        }

        private IUIControl CreateIntControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Label", "Name");
            input.AddInput("Value", 10);
            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("Int");
            panel.SetInput(input);
            return panel;
        }

        private IUIControl CreateFloatControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Label", "Name");
            input.AddInput("Value", 10f);
            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("Float");
            panel.SetInput(input);
            return panel;
        }

        private IUIControl CreateBooleanControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Description", "This is a Boolean Control");
            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("Boolean");
            panel.SetInput(input);
            return panel;
        }

        private IUIControl CreateSliderControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Description", "Strand slider control");
            input.AddInput("allowTextBox", true);
            input.AddInput("min", 50);
            input.AddInput("max", 150);
            input.AddInput("sliderType", "int");
            input.AddInput("adjustMinMax", false);
            input.AddInput("tickPlacement", "bottom");
            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("Slider");
            panel.SetInput(input);
            return panel;
        }

        private IUIControl CreateXamSliderControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Description", "Xam slider control");
            input.AddInput("allowTextBox", true);
            input.AddInput("min", 50);
            input.AddInput("max", 150);
            input.AddInput("sliderType", "int");
            input.AddInput("adjustMinMax", false);
            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("XamSlider");
            panel.SetInput(input);
            return panel;
        }

        private IUIControl CreateRangeSliderControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Description", "Range slider control");
            input.AddInput("allowTextBox", true);
            input.AddInput("sliderType", "int");
            input.AddInput("adjustMinMax", false);
            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("RangeSlider");
            panel.SetInput(input);
            return panel;
        }

        private IUIControl CreateListControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Description", "List control");
            input.AddInput("options", new List<object> {"Test1", "Test2", "Test3", "Test4", "Test5" });
            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("List");
            panel.SetInput(input);
            return panel;
        }

        private IUIControl CreateRadioCardControl()
        {
            Dictionary<string, object> labels = new Dictionary<string, object>();

            IUIControl StringControl = CreateStringControl();

            IUIControl IntControl = CreateIntControl();

            IUIControl FloatControl = CreateFloatControl();

            IUIControl BooleanControl = CreateBooleanControl();

            IUIControl SliderControl = CreateSliderControl();

            IUIControl XamSliderControl = CreateXamSliderControl();

            IUIControl RangeSliderControl = CreateRangeSliderControl();

            IUIControl ListControl = CreateListControl();

            labels.Add("String Control", StringControl);
            labels.Add("Int Control", IntControl);
            labels.Add("Float Control", FloatControl);
            labels.Add("Boolean Control", BooleanControl);
            //labels.Add("Slider Control", SliderControl);
            labels.Add("Xam Slider Control", XamSliderControl);
            labels.Add("Range Slider Control", RangeSliderControl);
            labels.Add("List Control", ListControl);
            UIInput input = new UIInput();
            input.AddInput("Options", labels);

            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("RadioCard");
            panel.SetInput(input);
            return panel;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void TestImputation()
        {
            TestAlgorithm.TestImputationAlgorithm(this.UnityContainer);
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
