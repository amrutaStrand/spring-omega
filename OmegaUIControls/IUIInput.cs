using System;
using System.Collections.Generic;

namespace Agilent.MHDA.Omega
{
    public interface IUIInput
    {
        Dictionary<string, object> Parameters { get; set; }

        object GetInput(string key);
        object GetInput(string key, object defaultValue);
        //string GetStringInput(string key);
        //int GetIntInput(string key);
        void AddInput(string key, object value);

        bool HasParameter(string key);

    }
}
