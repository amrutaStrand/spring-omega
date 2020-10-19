﻿using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using OmegaUIControls.OmegaUIUtils;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// FileControl is a container encapsulating a <see cref="Label"/>, <see cref="TextBox"/> and 
    /// <see cref="Button"/>. The button click launches a <see cref="OpenFileDialog"/> or <see cref="SaveFileDialog"/> 
    /// depending on the input parameters. The selected file path is displayed in the text box.
    /// Parameters of this class are<list type="bullet" >
    /// <item>Description (String) : string shown in the label</item>
    /// <item>dialogType (String) : "open" or "save"</item>
    /// <item>currentDirectory (String) : intial directory in which the dialog is launched</item>
    /// <item>fileFilters (String) : determines what types of files are displayed in the dialog. 
    /// e.g. "Text files | *.txt"</item>
    /// <item>enableMultipleSelection (bool) : flag indicating whether the dialog allows users 
    /// to select multiple files </item>
    /// <item>buttonLabel (String) : string shown in the button</item>
    ///</list> 
    /// </summary>
    class FileControl : AbstractUIControl
    {
        protected Label label;
        protected FileDialog dialog;
        protected Button button;
        protected TextBox textBox;

        /// <summary>
        /// Value property of FileControl is the file path(s) seleted in the <see cref="FileDialog"/>. The get 
        /// method returns a list of strings, while the set method accepts a string.
        /// </summary>
        public override object Value
        {
            get
            {
                return dialog.FileNames;
            }
            set
            {
                string setpath = value as string;
                dialog.FileName = setpath;
                textBox.Text = setpath;
            }
        }

        /// <summary>
        /// A <see cref="Label"/>, <see cref="TextBox"/> and <see cref="Button"/> are added in a <see cref="LayoutPanel"/>.
        /// </summary>
        public override void CreateUIElement()
        {
            CreateLabel();
            CreateDialog();
            CreateButton();

            var panel = new LayoutPanel(1, 3);
            panel.Add(label, 1);
            panel.Add(textBox, 3);
            panel.Add(button, 1);
            panel.ChangeDimension(60, 600);
            UIElement = panel;
        }

        /// <summary>
        /// Creates a label which displays the Description of this control.
        /// </summary>
        protected void CreateLabel()
        {
            string description = (string)Input.GetInput("Description", "Select a file");

            label = new Label();
            label.VerticalAlignment = VerticalAlignment.Center;
            label.Margin = new Thickness(5);
            label.Content = description;
            label.RenderSize = UIConstants.LABEL_PREFERRED_SIZE;
        }

        /// <summary>
        /// Creates a <see cref="OpenFileDialog"/> or <see cref="SaveFileDialog"/>.
        /// </summary>
        protected void CreateDialog()
        {
            string dialogType = (string)Input.GetInput("dialogType", "open");
            
            if (dialogType.Equals("save"))
                dialog = new SaveFileDialog();
            else
                dialog = new OpenFileDialog();

            dialog.InitialDirectory = (string)Input.GetInput("currentDirectory", string.Empty);

            dialog.Filter = (string)Input.GetInput("fileFilters", string.Empty);

            if(dialogType.Equals("open") && Input.HasParameter("enableMultipleSelection"))
            {
                (dialog as OpenFileDialog).Multiselect = (bool)Input.GetInput("enableMultipleSelection");
            }
        }

        /// <summary>
        /// Creates a text box which stores the paths of selected files in the file dialog.
        /// </summary>
        protected void CreateTextBox()
        {
            textBox = new TextBox();
            textBox.Width = 200;
            textBox.TextWrapping = TextWrapping.Wrap;
            //textBox.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            textBox.Margin = new Thickness(5);
        }

        /// <summary>
        /// Creates a button which launchs a file dialog.
        /// </summary>
        protected void CreateButton()
        {
            button = new Button();
            button.Margin = new Thickness(5);
            button.Content = Input.GetInput("buttonLabel", "Browse");
            button.Click += Button_Click;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dialog.ShowDialog() == true)
            {
                textBox.Text = string.Join("; ", dialog.FileNames);
            }
        }

        /// <summary>
        /// Sets the initial directory of the <see cref="FileDialog"/> to the specified path.
        /// </summary>
        /// <param name="dir"></param>
        public void SetInitialDirectory(string dir)
        {
            dialog.InitialDirectory = dir;
        }
    }
}
