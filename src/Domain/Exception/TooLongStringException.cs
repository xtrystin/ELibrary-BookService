using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary_BookService.Domain.Exception
{
    internal class TooLongStringException : System.Exception
    {
        public TooLongStringException(string message) : base(message)
        {
        }
    }
}
