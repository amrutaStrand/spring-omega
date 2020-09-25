using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// Abstract implementation of the <see cref="IUIContainer"/> interface. 
    /// </summary>
    public abstract class AbstractUIContainer : AbstractUIControl, IUIContainer
    {
        /// <summary>
        /// This field stores the list of child controls in this container.
        /// </summary>
        protected IList<object> ControlList;

        /// <summary>
        /// Value property is a dictionary having mapping from control 'Id' to control 'Value' for each child control.
        /// </summary>
        public override object Value {
            get
            {
                int size = GetControlCount();

                Dictionary<string, object> map = new Dictionary<string, object>();

                for (int i = 0; i < size; i++)
                {
                    IUIControl control = GetControl(i);
                    string id = control.Id;
                    object value = control.Value;
                    map.Add(id, value);
                }

                return map;
            }
            set
            {
                IDictionary<string, object> map = new Dictionary<string, object>();

                if (value is IDictionary<string, object>)
                    map = value as IDictionary<string, object>;
                else
                {
                    //Throw some error
                }

                int size = GetControlCount();

                for (int i = 0; i < size; i++)
                {
                    IUIControl control = GetControl(i);
                    control.Value = map[control.Id];
                }
            }
        }

        public int GetControlCount()
        {
            return ControlList.Count();
        }

        public IUIControl GetControl(int index)
        {
            return ControlList[index] as IUIControl;
        }

        public IUIControl GetControl(string id)
        {
            int size = GetControlCount();

            for(int i = 0; i < size; i++)
            {
                IUIControl control = GetControl(i);

                if (control.Id.Equals(id))
                    return control;
            }

            return null;
        }

        /// <summary>
        /// Initializes the ControlList with the value of "controls" parameter in the Input.
        /// </summary>
        /// <param name="input"></param>
        public override void SetInput(IUIInput input)
        {
            Input = input;

            if (Input.HasParameter("controls"))
            {
                ControlList = new List<object>();

                IList<object> list = Input.GetInput("controls") as IList<object>;
                int size = list.Count();

                for(int i = 0; i < size; i++)
                {
                    object control = list[i];

                    if(control is IDictionary)
                    {
                        //Need to discuus
                    }

                    if (control is IUIControl)
                        ControlList.Add(control);
                    else
                        throw new Exception(control + "is not a control");
                }

            }
        }
    }
}
