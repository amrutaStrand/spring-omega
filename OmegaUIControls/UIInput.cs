﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilent.MHDA.Omega
{
    public class UIInput : IUIInput
    {
        public UIInput()
        {
            Parameters = new Dictionary<string, object>();
        }
        public Dictionary<string, object> Parameters { get; set; }

        public void AddInput(string key, object value)
        {
            if (Parameters == null)
                Parameters = new Dictionary<string, object>();
            Parameters.Add(key, value);
        }

        public object GetInput(string key)
        {
            if (Parameters == null)
                return null;
            if (!Parameters.ContainsKey(key))
                throw new InvalidKeyException(key);
            return Parameters[key];
        }

        public object GetInput(string key, object defaultValue)
        {
            if (Parameters == null)
                return null;
            return Parameters.ContainsKey(key) ? Parameters[key] : defaultValue;
        }

        public bool HasParameter(string key)
        {
            return Parameters.ContainsKey(key);
        }
    }

    [Serializable()]
    public class InvalidKeyException : Exception
    {
        public InvalidKeyException() : base() { }

        public InvalidKeyException(string key) : base(String.Format("Invalid Input Key: {0}", key))
        { }
    }
}
