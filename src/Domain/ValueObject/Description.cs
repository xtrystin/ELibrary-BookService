using ELibrary_BookService.Domain.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary_BookService.Domain.ValueObject
{
    public record Description : ValueObject<string>
    {
        public static readonly int MaxLength = 10000;

        public Description(string value)
        {
            if (value?.Length > MaxLength)
                throw new TooLongStringException($"Description cannot be longer than {MaxLength} characters");
            
            _value = value;
        }
    }
}
