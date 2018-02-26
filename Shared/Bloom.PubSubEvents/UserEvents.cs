using Prism.Events;

namespace Bloom.Events
{
    /// <summary>
    /// This event is published to show a change user modal.
    /// </summary>
    public class ShowChangeUserModalEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published to show a user profile modal.
    /// </summary>
    public class ShowUserProfileModalEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published when any properties of the active user have been updated.
    /// </summary>
    public class UserUpdatedEvent : PubSubEvent<object> { }
}
