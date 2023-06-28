using ELibrary_BookService.Domain.Repository;
using MassTransit;
using ServiceBusMessages;

namespace ELibrary_BookService.Consumers
{
    public class BookAvailabilityChangedConsumer : IConsumer<BookAvailabilityChanged>
    {
        private readonly IBookRepository _bookRepository;

        public BookAvailabilityChangedConsumer(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task Consume(ConsumeContext<BookAvailabilityChanged> context)
        {
            var message = context.Message;
            var book = await _bookRepository.GetAsync(message.BookId);
            if (book is not null)
            {
                book.ChangeBookAmount(message.Amount);
                await _bookRepository.UpdateAsync(book);
            }
        }
    }

    public class BookAvailabilityChangedBkConsumer : IConsumer<BookAvailabilityChangedBk>
    {
        private readonly IBookRepository _bookRepository;

        public BookAvailabilityChangedBkConsumer(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task Consume(ConsumeContext<BookAvailabilityChangedBk> context)
        {
            var message = context.Message;
            var book = await _bookRepository.GetAsync(message.BookId);
            if (book is not null)
            {
                book.ChangeBookAmount(message.Amount);
                await _bookRepository.UpdateAsync(book);
            }
        }
    }
}
