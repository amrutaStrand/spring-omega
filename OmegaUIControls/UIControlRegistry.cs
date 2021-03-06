﻿using Microsoft.Practices.Unity;
using OmegaUIControls;

namespace Agilent.MHDA.Omega
{
    /// <summary>
    /// This class is responsible for registering the controls
    /// </summary>
    public class UIControlRegistry
    {
        /// <summary>
        /// It registers the controls to the unity container instance
        /// </summary>
        /// <param name="container"></param>
        public static void RegisterUIControls(IUnityContainer container)
        {
            OmegaFactory.InitializeOmegaFactory(container);
            container.RegisterType<IUIControl, StringControl>("String");
            container.RegisterType<IUIControl, IntControl>("Int");
            container.RegisterType<IUIControl, FloatControl>("Float");
            container.RegisterType<IUIControl, DoubleControl>("Double");
            container.RegisterType<IUIControl, RadioCardUIControl>("RadioCard");
            container.RegisterType<IUIControl, BooleanControl>("Boolean");
            container.RegisterType<IUIControl, XamSliderControl>("XamSlider");
            container.RegisterType<IUIControl, RangeSliderControl>("RangeSlider");
            container.RegisterType<IUIControl, ListControl>("List");
            container.RegisterType<IUIControl, ComboBoxControl>("CheckBoxCombo");
            container.RegisterType<IUIControl, FileChooserControl>("FileControl");
            container.RegisterType<IUIControl, GroupControl>("Group");
            container.RegisterType<IUIControl, TabOmegaControl>("Tab");
        }
    }
}
