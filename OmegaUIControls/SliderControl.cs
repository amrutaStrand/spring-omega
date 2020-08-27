using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agilent.OpenLab.Spring.Omega;

namespace OmegaUIControls
{
    //Parameters of this class are
    //1. type (string) : sliderType should be any one "float" or "int"
    //2. min (number) : Minimum value of this slider
    //3. max (number) : Maximum value of this slider
    //4. leftLabel (string) : text that should display on the left side of the slider
    //5. rightLabel (string) : text that should display on the right side of the slider
    //6. allowTextBox (bool) : if this value is true then it will add a textfield next to slider and
    //                      the value of the textField is the slider current value
    class SliderControl : AbstractUIControl
    {
        protected string sliderType;
        public override object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void CreateUIElement()
        {
            init();
        }

        private void init()
        {
            sliderType = (string)Input.GetInput("SliderType");
            if (sliderType == null)
            {
                sliderType = "float";
            }
        }

        public override void SetInput(IUIInput input)
        {
            throw new NotImplementedException();
        }
    }
}
