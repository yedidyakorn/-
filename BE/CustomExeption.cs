using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class LogicException : Exception
    {
        public LogicException(string error) : base(error) { } 

        public LogicException( string field, string error) : base($"error {error} in field {field}"){}
    }
}
