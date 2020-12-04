using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilent.OpenLab.Spring.Omega
{
    public class MessageInfo
    {
        public static string INT_ERROR_MESSAGE = "Input should be in Integer format";
        public static string FLOAT_ERROR_MESSAGE = "Input should be in float format";
        public static string DOUBLE_ERROR_MESSAGE = "Input should be in double format";
        public static string STRING_ERROR_MESSAGE = "Input value should not be null";
        public static string OUT_OF_RANGE_ERROR_MESSAGE = "Input value must be between {0} and {1}";
        public static string LESS_THAN_ERROR_MESSAGE = "Input value must be less than {0}";
        public static string GREATER_THAN_ERROR_MESSAGE = "Input value must be greater than {0}";
    }
}
