using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using OmegaUIControls.OmegaUIUtils;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// Parameters of this class are<list type="bullet" >
    /// <item>options (Map or List) : items to be added in the left side list</item>
    /// <item>leftlabel (String) : Label to be added to left side list</item>
    /// <item>rightlabel (String) : Label to be added to right side list</item>
    /// <item>showBorder (bool)</item>
    /// <item>Description (String) : header to be shown in the border</item>
    ///</list>
    /// </summary>
    class ListControl : AbstractUIControl
    {
        private IList<object> elements;
        private ObservableCollection<object> leftElements;
        private ObservableCollection<object> rightElements;

        private ListBox leftList;
        private ListBox rightList;

        /// <summary>
        /// The Value property of a ListControl is a list of selected objects which are part of the 
        /// rightList.
        /// </summary>
        public override object Value
        {
            get
            {
                return new List<object>(rightElements);
            }
            set
            {
                if (!(value is IList))
                    throw new Exception("Value of ListControl should be an IList");

                IList data = value as IList;

                //clear both list and fill left
                leftElements.Clear();

                foreach (object o in elements)
                    leftElements.Add(o);

                rightElements.Clear();

                //move each item from left to right
                foreach(object o in data)
                {
                    if (leftElements.Remove(o))
                        rightElements.Add(o);
                }
            }
        }

        /// <summary>
        /// Adds two list boxes and buttons to a <see cref="LayoutPanel"/>.
        /// </summary>
        public override void CreateUIElement()
        {
            leftElements = new ObservableCollection<object>(elements);
            rightElements = new ObservableCollection<object>();

            LayoutPanel panel = new LayoutPanel(2, 3);

            bool showBorder = (bool)Input.GetInput("showBorder", true);

            string description = (string)Input.GetInput("Description", "Columns");

            if (showBorder)
                panel.AddBorder(description);

            AddLeftList(panel);
            AddControls(panel);
            AddRightList(panel);

            Value = Input.GetInput("Value", new List<string>());

            //panel.ChangeDimension(150, 800);
            UtilityMethods.SetPanelResources(panel);
            UIElement = panel;
        }

        /// <summary>
        /// Adds a stackpanel containing a label and a list box control (list of available items) 
        /// to the <see cref="LayoutPanel"/>.
        /// </summary>
        /// <param name="layoutPanel"></param>
        private void AddLeftList(LayoutPanel layoutPanel)
        {
            string leftLabel = (string)Input.GetInput("leftlabel", "Available Items");

            //Panel panel = new StackPanel();

            Label label = new Label();
            label.Content = leftLabel;
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.Margin = new Thickness(1);
            layoutPanel.Add(label, 4, 0, 0);
            //panel.Children.Add(label);

            leftList = new ListBox();
            leftList.SelectionMode = SelectionMode.Multiple;
            leftList.ItemsSource = leftElements;
            leftList.Height = 100;
            leftList.Width = 200;
            layoutPanel.Add(leftList, 4, 1, 0);
            //panel.Children.Add(leftList);

            //layoutPanel.Add(panel, 4);
        }

        /// <summary>
        /// Adds a stackpanel containing two buttons to move elements in the left and right list box.
        /// <param name="layoutPanel"></param>
        private void AddControls(LayoutPanel layoutPanel)
        {
            Panel panel = new StackPanel();
            panel.Margin = new Thickness(4);
            panel.VerticalAlignment = VerticalAlignment.Center;

            Button right = new Button();
            right.Margin = new Thickness(0, 0, 0, 4);
            right.Content = "--->";
            right.Click += Right_Click;
            panel.Children.Add(right);

            Button left = new Button();
            left.Content = "<---";
            left.Click += Left_Click;
            panel.Children.Add(left);
            layoutPanel.Add(panel, 1, 1, 1);

            //layoutPanel.Add(panel, 1);
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            IList tmp = new List<object>(rightList.SelectedItems as IEnumerable<object>);
            foreach (object o in tmp)
            {
                leftElements.Add(o);
                rightElements.Remove(o);
            }
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            IList tmp = new List<object>(leftList.SelectedItems as IEnumerable<object>);
            foreach (object o in tmp)
            {
                rightElements.Add(o);
                leftElements.Remove(o);
            }
        }

        /// <summary>
        /// Adds a stackpanel conatining a label and a list box control (list of selected items) 
        /// to the <see cref="LayoutPanel"/>.
        /// </summary>
        /// <param name="layoutPanel"></param>
        private void AddRightList(LayoutPanel layoutPanel)
        {
            string rightLabel = (string)Input.GetInput("rightLabel", "Selected Items");
            
            //Panel panel = new StackPanel();

            Label label = new Label();
            label.Content = rightLabel;
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.Margin = new Thickness(1);
            layoutPanel.Add(label, 4, 0, 2);
            //panel.Children.Add(label);

            rightList = new ListBox();
            rightList.SelectionMode = SelectionMode.Multiple;
            rightList.ItemsSource = rightElements;
            rightList.Height = 100;
            rightList.Width = 200;
            layoutPanel.Add(rightList, 4, 1, 2);
            //panel.Children.Add(rightList);

            //layoutPanel.Add(panel, 4);
        }

        public override void SetInput(IUIInput input)
        {
            base.SetInput(input);

            var param = Input.GetInput("options", new List<object>());

            if(param is IDictionary)
            {
                IEnumerable<object> val = ((IDictionary)param).Values as IEnumerable<object>;
                elements = new List<object>(val);
            }
            else if (param is IList<object>)
            {
                elements = new List<object>(param as IList<object>);
            }

            //disabledIndices = Input.HasParameter("disabledIndices") ? (IntArray)Input.GetInput("disabledIndices") : null;
        }

        /// <summary>
        /// Resets the options shown in the ListControl.
        /// </summary>
        /// <param name="newOptions"></param>
        public void ResetOptions(IList<object> newOptions)
        {
            elements = new List<object>(newOptions);

            leftElements.Clear();
            rightElements.Clear();

            foreach(object o in elements)
            {
                rightElements.Add(o);
            }
        }
    }
}
