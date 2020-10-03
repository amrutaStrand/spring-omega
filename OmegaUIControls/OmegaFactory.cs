using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// Factory class for creating omega controls.
    /// </summary>
    public class OmegaFactory
    {
        static IUnityContainer unityContainer;
        public static void InitializeOmegaFactory(IUnityContainer container)
        {
            unityContainer = container;
        }

        /// <summary>
        /// Creates a new omega component specified by the type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>The desired control.</returns>
        public static IUIControl CreateControl(string type)
        {
            try
            {
                IUIControl control = unityContainer.Resolve<IUIControl>(type);
                return control;
            }
            catch (Exception e)
            {
                //Throw or log the error: type could not be resolved
                return null;
            }
        }

        public static IUIControl CreateControl(IDictionary<string, object> parameters)
        {
            string type;
            try
            {
                if (parameters.ContainsKey("uitype"))
                    type = parameters["uitype"] as string;
                else
                    type = parameters["type"] as string;

                return CreateControl(type, parameters);
            }
            catch (Exception e)
            {
                //Throw or log the error: key not found
                return null;
            }
        }

        /// <summary>
        /// Creates a new omega control specified by the <paramref name="type"/> and initializes
        /// with the specified <paramref name="parameters"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IUIControl CreateControl(string type, IDictionary<string, object> parameters)
        {
            IUIControl control;

            //resolve the type from the container
            try
            {
                control = unityContainer.Resolve<IUIControl>(type);
            }
            catch (Exception e)
            {
                //Throw or log the error: type could not be resolved
                return null;
            }

            //create an instance of UIInput class
            UIInput input = new UIInput();
            foreach(KeyValuePair<string, object> pair in parameters)
            {
                input.AddInput(pair.Key, pair.Value);
            }

            control.SetInput(input);

            return control;
        }
    }
}
