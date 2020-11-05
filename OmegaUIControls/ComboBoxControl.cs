using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// A combo box of <see cref="CheckBox"/>. Parameter of this class is "options" which can be a <see cref="IDictionary"/> 
    /// or <see cref="IList"/> of strings. If "multiSelect" option is true, then combo box of checkboxes is shown.
    /// </summary>
    class ComboBoxControl : AbstractUIControl
    {
        private bool multiSelect;
        private IList<string> options;
        private IList<string> selectedOptions;
        private ObservableCollection<CheckBox> checkList;

        private ComboBox comboBox;
        private ToolTip toolTip;

        /// <summary>
        /// Value of CheckBoxComboControl is a list of selected item if multiSelect is true. Else
        /// it the selected value in the ComboBox.
        /// </summary>
        public override object Value {
            get
            {
                if (multiSelect)
                {
                    List<string> value = new List<string>(selectedOptions);
                    return value;
                }
                else
                {
                    return comboBox.SelectedItem;
                }
            }
            set
            {
                if (multiSelect)
                {
                    IList<string> v = value as IList<string>;
                    foreach (CheckBox check in checkList)
                    {
                        check.IsChecked = v.Contains(check.Content.ToString()) ? true : false;
                    }
                    comboBox.Text = string.Join(", ", selectedOptions);
                }
                else
                {
                    comboBox.SelectedItem = value;
                }
            } 
        }

        /// <summary>
        /// UIElement of <see cref="ComboBoxControl"/> is a <see cref="ComboBox"/> whose item source is
        /// an <see cref="ObservableCollection{T}"/> of <see cref="CheckBox"/>.
        /// </summary>
        public override void CreateUIElement()
        {
            comboBox = new ComboBox();
            comboBox.IsEditable = true;
            comboBox.IsReadOnly = true;
            comboBox.DropDownClosed += ComboBox_DropDownClosed;

            if (multiSelect)
            {
                comboBox.Text = options[0];
                selectedOptions = new List<string>();
                checkList = new ObservableCollection<CheckBox>();
                foreach (string item in options)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.Checked += CheckBox_Checked;
                    checkBox.Unchecked += CheckBox_Unchecked;
                    checkBox.Content = item;
                    checkList.Add(checkBox);
                }
                checkList[0].IsChecked = true;
                comboBox.ItemsSource = checkList;
            }
            else
            {
                comboBox.SelectedItem = options[0];
                comboBox.ItemsSource = new List<string>(options);
            }

            if (Input.HasParameter("Value"))
                Value = Input.GetInput("Value");

            toolTip = new ToolTip();
            toolTip.Content = multiSelect ? string.Join(", ", selectedOptions) : comboBox.SelectedItem;
            comboBox.ToolTip = toolTip;

            comboBox.Width = 200;
            SetResources(comboBox);
            UIElement = comboBox;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            selectedOptions.Remove(checkBox.Content as string);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            selectedOptions.Add(checkBox.Content as string);
        }

        //Sets the text and tooltip of the combo box to the selected items.
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if(multiSelect)
                comboBox.Text = string.Join(", ", selectedOptions);
            toolTip.Content = multiSelect ? string.Join(", ", selectedOptions) : comboBox.SelectedItem;
        }

        public override void SetInput(IUIInput input)
        {
            base.SetInput(input);

            var param = Input.GetInput("options", new List<string>()) as IList<string>;

            if (param is IDictionary)
            {
                IEnumerable<string> val = ((IDictionary)param).Values as IEnumerable<string>;
                options = new List<string>(val);
            }
            else if (param is IList)
            {
                options = param as IList<string>;
            }

            multiSelect = (bool)Input.GetInput("multiSelect", false);
        }
    }
}
