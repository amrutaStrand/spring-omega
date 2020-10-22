namespace Agilent.OpenLab.UiModule1
{
    #region

    using Agilent.OpenLab.Framework.UI.Module;
    using Agilent.OpenLab.Spring.Omega;
    using global::OpenLab.Agilent.Spring.Algorithm;
    using Microsoft.Practices.Unity;
    using System.Collections;
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
            input.AddInput("min", -100);
            input.AddInput("max", 100);
            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("Int");
            panel.SetInput(input);
            return panel;
        }

        private IUIControl CreateFloatControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Label", "Name");
            input.AddInput("Value", 10f);
            input.AddInput("min", -100);
            input.AddInput("max", 100);
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

        //private IUIControl CreateXamSliderControl()
        //{
        //    UIInput input = new UIInput();
        //    input.AddInput("Description", "Slider control");
        //    input.AddInput("allowTextBox", true);
        //    input.AddInput("min", 50);
        //    input.AddInput("max", 150);
        //    input.AddInput("sliderType", "int");
        //    input.AddInput("adjustMinMax", false);
        //    IUIControl panel = this.UnityContainer.Resolve<IUIControl>("XamSlider");
        //    panel.SetInput(input);
        //    return panel;
        //}

        private IUIControl CreateXamSliderControl()
        {
            UIInput input = new UIInput();
            Dictionary<string, object> pairs = new Dictionary<string, object> 
            {
                    { "Description", "Strand slider control" },
                    { "allowTextBox", true},
                    { "min", 200 },
                    { "max", 500},
                    { "Value", 300}
            };
            IUIControl panel = OmegaFactory.CreateControl("XamSlider", pairs);
            //IUIControl panel = OmegaFactory.CreateControl("XamSlider");
            return panel;
        }

        private IUIControl CreateRangeSliderControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Description", "Range slider control");
            input.AddInput("allowTextBox", true);
            input.AddInput("min", 500);
            input.AddInput("max", 900);
            input.AddInput("Value", new Dictionary<string, float> { {"min", 546 }, {"max", 823 } });
            input.AddInput("sliderType", "int");
            input.AddInput("adjustMinMax", false);
            input.AddInput("showAbsolute", false);
            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("RangeSlider");
            panel.SetInput(input);
            return panel;
        }

        private IUIControl CreateListControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Description", "List control");
            input.AddInput("options", new List<object> {"Sample-1", "Sample-2", "Sample-3", "Sample-4", "Sample-5" });
            input.AddInput("Value", new List<object> { "Sample-1", "Sample-2", "Sample-6" });
            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("List");
            panel.SetInput(input);
            return panel;
        }

        private IUIControl CreateGroupControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Description", "Horizontal group control");
            input.AddInput("controls", new List<object> { CreateStringControl(), CreateBooleanControl(), CreateCheckBoxCombo() });
            input.AddInput("orientation", "horizontal");
            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("Group");
            panel.SetInput(input);
            return panel;
        }

        private IUIControl CreateVGroupControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Description", "Vertical group control");
            //input.AddInput("controls", new List<object> { CreateGroupControl(), CreateRangeSliderControl(), CreateListControl() });
            input.AddInput("controls", new List<object> { CreateStringControl(), CreateBooleanControl(), CreateCheckBoxCombo() });
            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("Group");
            panel.SetInput(input);
            return panel;
        }

        private IUIControl CreateBoxCombo()
        {
            UIInput input = new UIInput();
            input.AddInput("Description", "Check Box Combo");
            input.AddInput("options", new List<string> { "Mumbai", "Chennai", "Kolkata", "Delhi" });
            input.AddInput("Value", "Kolkata");
            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("CheckBoxCombo");
            panel.SetInput(input);
            return panel;
        }

        private IUIControl CreateCheckBoxCombo()
        {
            UIInput input = new UIInput();
            input.AddInput("Description", "Check Box Combo");
            input.AddInput("options", new List<string> { "Mumbai", "Chennai", "Kolkata", "Delhi" });
            input.AddInput("multiSelect", true);
            input.AddInput("Value", new List<string> { "Bangalore", "Chennai", "Kolkata" });
            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("CheckBoxCombo");
            panel.SetInput(input);
            return panel;
        }

        private IUIControl CreateFileControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Description", "File control");
            input.AddInput("enableMultipleSelection", true);
            input.AddInput("fileFilters", "txt files (*.txt)|*.txt|Images (*.png)|*.png|All files (*.*)|*.*");
            //input.AddInput("dialogType", "save");
            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("FileControl");
            panel.SetInput(input);
            return panel;
        }

        private IUIControl CreateTabControl()
        {
            UIInput input = new UIInput();
            input.AddInput("Description", "Demo tab control");
            input.AddInput("controls", new List<object> { CreateStringControl(), CreateBooleanControl(), CreateCheckBoxCombo() });
            IUIControl panel = this.UnityContainer.Resolve<IUIControl>("Tab");
            panel.SetInput(input);
            return panel;
        }

        private OmegaDialog CreateOmegaDialog()
        {
            Dictionary<string, IDictionary> itemList = new Dictionary<string, IDictionary>();
            itemList.Add("CheckBoxCombo", new Dictionary<string, object>
            {
                {"options", new List<string>{ "Control-1", "Validation-1", "Clinical-1", "Clinical-2" } }
            });
            itemList.Add("XamSlider", new Dictionary<string, object>
            {
                {"allowTextBox", true }
            });
            itemList.Add("RangeSlider", new Dictionary<string, object>
            {
                {"allowTextBox", true }
            });
            List<IDictionary> uiLayout = new List<IDictionary>();
            uiLayout.Add(new Dictionary<string, object> {
                { "title", "Test Omega Dialog - Tab 1"},
                { "contents", new List<object>{ "Boolean", "CheckBoxCombo", "XamSlider" } }
            });
            //uiLayout.Add(new Dictionary<string, object> {
            //    { "title", "Test Omega Dialog - Tab 2"},
            //    { "contents", new List<object>{ "RangeSlider", "FileControl" } }
            //});
            OmegaDialog dialog = new OmegaDialog(null, itemList, uiLayout);
            return dialog;
        }

        private Button CreateButton()
        {
            Button button = new Button();
            button.Width = 300;
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.Content = "Launch Omega Dialog";
            button.Click += Button_Click;
            return button;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OmegaDialog dialog = CreateOmegaDialog();
            dialog.ShowDialog();
        }

        private IUIControl CreateRadioCardControl()
        {
            Dictionary<string, object> labels = new Dictionary<string, object>();

            IUIControl StringControl = CreateStringControl();

            IUIControl IntControl = CreateIntControl();

            IUIControl FloatControl = CreateFloatControl();

            IUIControl BooleanControl = CreateBooleanControl();

            IUIControl XamSliderControl = CreateXamSliderControl();

            IUIControl RangeSliderControl = CreateRangeSliderControl();

            IUIControl ListControl = CreateListControl();

            IUIControl GroupControl = CreateGroupControl();

            IUIControl VGroupControl = CreateVGroupControl();

            IUIControl BoxCombo = CreateBoxCombo();

            IUIControl CheckBoxCombo = CreateCheckBoxCombo();

            IUIControl FileControl = CreateFileControl();

            IUIControl TabControl = CreateTabControl();

            Button launchButton = CreateButton();

            labels.Add("String Control", StringControl);
            labels.Add("Int Control", IntControl);
            labels.Add("Float Control", FloatControl);
            labels.Add("Boolean Control", BooleanControl);
            labels.Add("Slider Control", XamSliderControl);
            labels.Add("Range Slider Control", RangeSliderControl);
            labels.Add("List Control", ListControl);
            labels.Add("Single-select Combo Box", BoxCombo);
            labels.Add("Multi-select Combo Box", CheckBoxCombo);
            labels.Add("File Control", FileControl);
            labels.Add("Horizontal Group Control", GroupControl);
            labels.Add("Vertical Group Control", VGroupControl);
            labels.Add("Tab Control", TabControl);
            labels.Add("Omega Dialog", launchButton);

            UIInput input = new UIInput();
            input.AddInput("Options", labels);
            input.AddInput("Value", "Slider Control");

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
