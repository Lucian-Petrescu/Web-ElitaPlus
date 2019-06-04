using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assurant.ElitaPlus.External.Indix.Exceptions
{
    public class InvalidRequestFieldException: Exception
    {
        public InvalidRequestFieldException() : base("At least one of the fields from request is invalid") { }
    }
}
