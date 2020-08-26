using System.Collections.Generic;
using System.Windows;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractUIControl : IUIControl
    {
        /// <summary>
        /// 
        /// </summary>
        public abstract object Value { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public IUIInput Input { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected UIElement UIElement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isEnabled"></param>
        public void SetEnabled(bool isEnabled)
        {
            UIElement.IsEnabled = isEnabled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valuePairs"></param>
        public abstract void SetInput(IUIInput input);

        /// <summary>
        /// 
        /// </summary>
        public abstract void CreateUIElement();

        /// <summary>
        /// Get's the UIElement if it is already created otherwise create one
        /// </summary>
        /// <returns></returns>
        public virtual UIElement GetUIElement()
        {
            if (UIElement == null)
                CreateUIElement();
            return UIElement;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool Validate(object value) // validation messages
        {
            return true;
        }
    }
}
