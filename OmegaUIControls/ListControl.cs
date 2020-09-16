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
    //Parameters of this class are
    //1. options (Map or List) : items to be added in the left side list    
    //2. leftlabel (String) : Label to be added to left side list.
    //3. rightlabel (String) : Label to be added to right side list.
    class ListControl : AbstractUIControl
    {
        private IList<object> elements;
        //private IList<object> leftElements;
        private ObservableCollection<object> leftElements;
        //private IList<object> rightElements;
        private ObservableCollection<object> rightElements;
        private SortedIntArray left;
        private SortedIntArray right;

        private ListBox leftList;
        private ListBox rightList;
        protected IntArray disabledIndices;

        public override void CreateUIElement()
        {
            int size = elements.Count;
            left = new SortedIntArray(size);
            right = new SortedIntArray(size);

            //leftElements = new List<object>(elements);
            leftElements = new ObservableCollection<object>(elements);
            //rightElements = new List<object>();
            rightElements = new ObservableCollection<object>();

            for (int i = 0; i < size; i++)
                left.Add(i);

            LayoutPanel panel = new LayoutPanel(1, 3);

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

            disabledIndices = Input.HasParameter("disabledIndices") ? (IntArray)Input.GetInput("disabledIndices") : null;

        }
    }
}
