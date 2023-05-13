using ELibrary_BookService.Domain.Exception;

namespace ELibrary_BookService.Domain.ValueObject
{
    public record Title : ValueObject<string>
    {
        public static readonly int MaxLength = 200;

        public Title(string value)
        {
            if (value?.Length > MaxLength)
                throw new TooLongStringException($"Title cannot be longer than {MaxLength} characters");

            _value = value;
        }
    }
}
