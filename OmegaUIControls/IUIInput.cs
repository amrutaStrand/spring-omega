using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilent.OpenLab.Spring.Omega
{
    public interface IUIInput
    {
        Dictionary<string, object> Parameters { get; set; }

        object GetInput(string key);
        //string GetStringInput(string key);
        //int GetIntInput(string key);
        void AddInput(string key, object value);

    }
}
