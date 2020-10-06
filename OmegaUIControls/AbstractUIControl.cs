using System.Windows;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// Abstract implementation of <see cref="IUIControl"/> interface.
    /// </summary>
    public abstract class AbstractUIControl : IUIControl
    {
        /// <summary>
        /// Constructor to set parameters to default values.
        /// </summary>
        public AbstractUIControl() => SetInput(new UIInput());

        public virtual object Value { get; set; }

        public IUIInput Input { get; set; }

        public string Id { get; set; }

        /// <summary>
        /// The Property for <see cref="UIElement"/> stored in the control.
        /// </summary>
        protected UIElement UIElement { get; set; }

        public void SetEnabled(bool isEnabled)
        {
            UIElement.IsEnabled = isEnabled;
        }

        public virtual void SetInput(IUIInput input) => Input = input;

        public abstract void CreateUIElement();

        /// <summary>
        /// Gets the UIElement if it is already created otherwise create one
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
        public virtual bool IsValid(object value) // validation messages
        {
            return true;
        }

        public virtual void ShowValidationError()
        {

        }

    }
}
