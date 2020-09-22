using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using Agilent.OpenLab.Spring.Omega;
using OmegaUIControls.Cube;
using System.Windows;
using System.Windows.Controls;
using OmegaUIControls.OmegaUIUtils;
using System.ComponentModel;

namespace OmegaUIControls
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
        //private IList<object> leftElements;
        private ObservableCollection<object> leftElements;
        //private IList<object> rightElements;
        private ObservableCollection<object> rightElements;

        private ListBox leftList;
        private ListBox rightList;

        /// <summary>
        /// The Value property of a ListControl is a list of selected objects which are part of the rightList.
        /// </summary>
        public override object Value
        {
            get
            {
                return new List<object>(rightElements);
            }
            set
            {
                if (!(value is List<object>))
                    throw new Exception("Value of ListControl should be a list of objects");

                List<object> data = value as List<object>;

                //clear right and fill left
                leftElements = new ObservableCollection<object>(elements);
                rightElements.Clear();

                //move each item from left to right
                for(int i = 0; i < data.Count; i++)
                {
                    leftElements.Remove(data[i]);
                    rightElements.Add(data[i]);
                }

            }
        }

        /// <summary>
        /// Adds two list boxes and buttons to a <see cref="LayoutPanel"/>.
        /// </summary>
        public override void CreateUIElement()
        {
            //leftElements = new List<object>(elements);
            leftElements = new ObservableCollection<object>(elements);
            //rightElements = new List<object>();
            rightElements = new ObservableCollection<object>();

            LayoutPanel panel = new LayoutPanel(1, 3);
            panel.Margin = new Thickness(10);

            bool showBorder = Input.HasParameter("showBorder") ? (bool)Input.GetInput("showBorder") : true;

            string description = Input.HasParameter("Description") ? (string)Input.GetInput("Description") : "Columns";

            if (showBorder)
                panel.AddBorder(description);

            AddLeftList(panel);
            AddControls(panel);
            AddRightList(panel);

            panel.ChangeDimension(150, 800);

            UIElement = panel;
        }

        /// <summary>
        /// Adds a stackpanel containing a label and a list box control (list of available items) 
        /// to the <see cref="LayoutPanel"/>.
        /// </summary>
        /// <param name="layoutPanel"></param>
        private void AddLeftList(LayoutPanel layoutPanel)
        {
            string leftLabel = Input.HasParameter("leftlabel") ? (string)Input.GetInput("leftlabel") : "Available Items";

            Panel panel = new StackPanel();

            Label label = new Label();
            label.Content = leftLabel;
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.Margin = new Thickness(1);
            panel.Children.Add(label);

            leftList = new ListBox();
            leftList.SelectionMode = SelectionMode.Multiple;
            leftList.ItemsSource = leftElements;
            leftList.Height = 100;
            panel.Children.Add(leftList);

            layoutPanel.Add(panel, 4);
        }

        /// <summary>
        /// Adds a stackpanel containing two buttons to move elements in the left and right list box.
        /// <param name="layoutPanel"></param>
        private void AddControls(LayoutPanel layoutPanel)
        {
            Panel panel = new StackPanel();

            Button right = new Button();
            right.Margin = new Thickness(4, 15, 4, 4);
            right.Content = "--->";
            right.Click += Right_Click;
            panel.Children.Add(right);

            Button left = new Button();
            left.Margin = new Thickness(4);
            left.Content = "<---";
            left.Click += Left_Click;
            panel.Children.Add(left);

            layoutPanel.Add(panel, 1);
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            IList tmp = new List<object>(rightList.SelectedItems as IEnumerable<object>);
            foreach (object o in tmp)
            {
                leftElements.Add(o);
                //(leftList.ItemsSource as ObservableCollection<object>).Add(o);
                rightElements.Remove(o);
                //(rightList.ItemsSource as ObservableCollection<object>).Remove(o);
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
            string rightLabel = Input.HasParameter("rightLabel") ? (string)Input.GetInput("rightLabel") : "Selected Items";
            
            Panel panel = new StackPanel();

            Label label = new Label();
            label.Content = rightLabel;
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.Margin = new Thickness(1);
            panel.Children.Add(label);

            rightList = new ListBox();
            rightList.SelectionMode = SelectionMode.Multiple;
            rightList.ItemsSource = rightElements;
            rightList.Height = 100;
            panel.Children.Add(rightList);

            layoutPanel.Add(panel, 4);
        }

        public override void SetInput(IUIInput input)
        {
            Input = input;

            var param = Input.GetInput("options");

            if(param is IDictionary)
            {
                IEnumerable<object> val = ((IDictionary)param).Values as IEnumerable<object>;
                elements = new List<object>(val);
            }
            else if (param is IList<object>)
            {
                elements = param as IList<object>;
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
            int size = elements.Count;

            leftElements.Clear();
            rightElements.Clear();

            for(int i = 0; i < size; i++)
            {
                rightElements.Add(elements[i]);
            }
        }
    }
}
