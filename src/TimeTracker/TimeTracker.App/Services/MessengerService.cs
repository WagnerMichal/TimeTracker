using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Services.Interfaces;

namespace TimeTracker.App.Services;

public class MessengerService : IMessengerService
{
    public IMessenger Messenger { get; }

    public MessengerService(IMessenger messenger)
    {
        Messenger = messenger;
    }

    public void Send<TMessage>(TMessage message)
        where TMessage : class
    {
        Messenger.Send(message);
    }
}