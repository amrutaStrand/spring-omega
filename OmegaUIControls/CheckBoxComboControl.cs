using System;
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
        private IList<string> options;
        private ObservableCollection<CheckBox> checkList;

        public override object Value {
            get
            {
                List<string> value = new List<string>();
                foreach(CheckBox check in checkList)
                {
                    if ((bool)check.IsChecked)
                        value.Add((string)check.Content);
                }
                return value;
            }
            set
            {
                IList<string> v = value as IList<string>;
                foreach (CheckBox check in checkList)
                {
                    check.IsChecked = v.Contains(check.Content.ToString()) ? true : false;
                }
            } 
        }

        /// <summary>
        /// UIElement of <see cref="CheckBoxComboControl"/> is a <see cref="ComboBox"/> whose item source is
        /// an <see cref="ObservableCollection{CheckBox}"/>
        /// </summary>
        public override void CreateUIElement()
        {
            ComboBox comboBox = new ComboBox();
            comboBox.IsEditable = true;
            comboBox.IsReadOnly = true;
            comboBox.Text = "Choose one or more options";
            checkList = new ObservableCollection<CheckBox>();
            foreach(string item in options)
            {
                CheckBox check = new CheckBox();
                check.Content = item;
                checkList.Add(check);
            }
            comboBox.ItemsSource = checkList;
            comboBox.Width = 250;
            UIElement = comboBox;
        }

        public override void SetInput(IUIInput input)
        {
            Input = input;

            var param = Input.GetInput("options") as IList<string>;

            if (param is IDictionary)
            {
                IEnumerable<string> val = ((IDictionary)param).Values as IEnumerable<string>;
                options = new List<string>(val);
            }
            else if (param is IList)
            {
                options = param as IList<string>;
            }
        }
    }
}
