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
            return CreateControl(type, new UIInput());
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
            catch (InvalidKeyException e)
            {
                //Throw or log the error: "type" key not found
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
            //create an instance of UIInput class
            UIInput input = new UIInput();
            foreach (KeyValuePair<string, object> pair in parameters)
            {
                input.AddInput(pair.Key, pair.Value);
            }

            return CreateControl(type, input);
        }

        /// <summary>
        /// Creates a new omega control specified by the <paramref name="type"/> and initializes
        /// with the specified <paramref name="input"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IUIControl CreateControl(string type, IUIInput input)
        {
            IUIControl control;

            //resolve the type from the container
            try
            {
                control = unityContainer.Resolve<IUIControl>(type);
            }
            catch (ResolutionFailedException e)
            {
                //Throw or log the error: type could not be resolved
                return null;
            }

            control.SetInput(input);

            return control;
        }
    }
}
