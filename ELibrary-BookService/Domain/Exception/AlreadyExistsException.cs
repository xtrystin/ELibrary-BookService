using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary_BookService.Domain.Exception
{
    public class AlreadyExistsException : System.Exception
    {
        public AlreadyExistsException(string message) : base(message) 
        {
        }
    }
}
