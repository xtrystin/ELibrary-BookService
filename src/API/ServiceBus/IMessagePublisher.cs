namespace ELibrary_BookService.ServiceBus;

public interface IMessagePublisher
{
    Task Publish<T>(T message);
}
