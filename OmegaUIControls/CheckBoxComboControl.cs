﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// A combo box of <see cref="CheckBox"/>. Parameter of this class is "options" which
    /// can be a <see cref="IDictionary"/> or <see cref="IList"/> of strings.
    /// </summary>
    class CheckBoxComboControl : AbstractUIControl
    {
        private bool multiSelect;
        private IList<string> options;
        private IList<string> selectedOptions;
        private ObservableCollection<CheckBox> checkList;
        private ComboBox comboBox;

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
        /// UIElement of <see cref="CheckBoxComboControl"/> is a <see cref="ComboBox"/> whose item source is
        /// an <see cref="ObservableCollection{T}"/> of <see cref="CheckBox"/>.
        /// </summary>
        public override void CreateUIElement()
        {
            comboBox = new ComboBox();
            
            if (multiSelect)
            {
                comboBox.IsEditable = true;
                comboBox.IsReadOnly = true;
                comboBox.Text = options[0];
                comboBox.DropDownClosed += ComboBox_DropDownClosed;
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

            comboBox.Margin = new Thickness(10);
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

        //Sets the text of the combo box to its initial value.
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            comboBox.Text = string.Join(", ", selectedOptions);
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
