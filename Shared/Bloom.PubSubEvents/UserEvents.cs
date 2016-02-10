using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.PubSubEvents
{
    public class ShowChangeUserModalEvent : PubSubEvent<object> { }

    public class ShowUserProfileModalEvent : PubSubEvent<object> { }

    public class UserUpdatedEvent : PubSubEvent<object> { }
}
