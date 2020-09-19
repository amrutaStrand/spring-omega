
namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// Interface for container control which contains multiple controls.
    /// </summary>
    public interface IUIContainer : IUIControl
    {
        /// <summary>
        /// Returns the number of components in this container.
        /// </summary>
        /// <returns></returns>
        int GetControlCount();

        /// <summary>
        /// Returns the control at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IUIControl GetControl(int index);

        /// <summary>
        /// Returns the component with the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IUIControl GetControl(string id);
    }
}
