using System.Collections.Generic;
using System.Windows;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// Interface that defines ui control for a datatype.
    /// </summary>
    public interface IUIControl
    {
        /// <summary>
        /// The Property for ID of the control
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// The Property for Value of the control
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// The Property for Parameters of the control
        /// </summary>
        IUIInput Input { get; set; }
        
        /// <summary>
        /// A method to enable / disable the UIControl
        /// </summary>
        /// <param name="isEnabled"></param>
        void SetEnabled(bool isEnabled);
        
        /// <summary>
        /// A method to set the respective parameters to the UIControl
        /// </summary>
        /// <param name="valuePairs"></param>
        void SetInput(IUIInput input);
        
        /// <summary>
        /// It creates a UIElements based on the parameter types
        /// </summary>
        void CreateUIElement();
        
        /// <summary>
        /// Get's the UIElement that has been created.
        /// </summary>
        /// <returns></returns>
        UIElement GetUIElement();
    }
}
