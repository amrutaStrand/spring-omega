using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// Dialog to display omega controls.
    /// </summary>
    public class OmegaDialog : SimpleDialog
    {
        protected IDictionary itemList;

        protected IList uiLayout;

        protected Dictionary<string, IUIControl> ControlMap = new Dictionary<string, IUIControl>();

        public IUIControl TabbedComponent { get; private set; }

        #region Constructors
        protected OmegaDialog() : this(null)
        {

        }

        protected OmegaDialog(bool isHelp) : this(null, isHelp)
        {

        }

        protected OmegaDialog(Window parent) : base(parent, GetProperties(false))
        {

        }

        protected OmegaDialog(Window parent, bool isHelp) : base(parent, GetProperties(isHelp))
        {

        }

        protected OmegaDialog(Window parent, IDictionary<string, object> properties)
            :base(parent, properties)
        {

        }

        public OmegaDialog(Window parent, IDictionary itemList, IList uiLayout)
            : base(parent, GetProperties(false))
        {
            initialize(itemList, uiLayout);
        }
        #endregion

        protected void initialize(IDictionary itemList, IList uiLayout)
        {
            this.itemList = itemList;
            this.uiLayout = uiLayout;
            DialogContent = CreateContentPane(uiLayout);
            this.ResizeMode = ResizeMode.CanResize;
        }

        protected Panel CreateContentPane(IList ui)
        {
            List<object> controls = new List<object>();
            IDictionary map = null;

            foreach (object obj in ui)
            {
                map = obj as IDictionary;
                map.Add("showBorder", false);
                controls.Add(CreateGroup(map));
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"id", "properties"},
                {"Description", "Properties" },
                {"controls", controls }
            };

            IUIControl omega = null;

            if (controls.Count == 1)
            {
                parameters["Description"] = map.Contains("title") ? map["title"] : "Properties";
                omega = OmegaFactory.CreateControl("Group", parameters);
            }
            else
                omega = OmegaFactory.CreateControl("Tab", parameters);

            TabbedComponent = omega;
            Grid panel = new Grid();
            panel.Children.Add(omega.GetUIElement());
            return panel;
        }

        private IUIControl CreateGroup(IDictionary map)
        {
            string title = map["title"] as string;
            IList<object> contents = map["contents"] as IList<object>;

            List<object> controls = new List<object>();

            foreach (object obj in contents)
            {
                IUIControl control = null;

                if (obj is IDictionary)
                    control = CreateGroup(obj as IDictionary);
                else if (obj is string)
                    control = CreateControl(obj as string);

                controls.Add(control);
            }

            IDictionary<string, object> newMap = new Dictionary<string, object>(map as IDictionary<string, object>);
            newMap.Remove("title");
            newMap.Remove("contents");
            newMap.Add("id", title);
            newMap.Add("Description", title);
            newMap.Add("controls", controls);

            return OmegaFactory.CreateControl("Group", newMap);
        }

        private IUIControl CreateControl(string id)
        {
            IUIControl control = null;
            if (itemList.Contains(id))
            {
                Dictionary<string, object> item = itemList[id] as Dictionary<string, object>;
                control = OmegaFactory.CreateControl(id, item);
            }
            else
            {
                control = OmegaFactory.CreateControl(id);
            }
            ControlMap.Add(id, control);
            return control;
        }

        public IUIControl GetControl(string id)
        {
            return ControlMap[id];
        }

        private static IDictionary<string, object> GetProperties(bool isHelp)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("title", "Properties");
            keyValues.Add("hasHelp", isHelp);
            return keyValues;
        }
    }
}
