using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Model
{
    public class InterfaceHNException : Exception
    {
        public InterfaceHNException(string msg) : base(msg)
        {

        }
    }
}

