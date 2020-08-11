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
        public abstract Dictionary<string, object> Parameters { get; set; }
        
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
        public abstract void SetParameters(Dictionary<string, object> valuePairs);

        /// <summary>
        /// 
        /// </summary>
        public abstract void CreateUIElement();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract UIElement GetUIElement();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool Validate(object value);
    }
}
