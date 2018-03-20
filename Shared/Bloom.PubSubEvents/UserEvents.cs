using Bloom.State.Domain.Models;
using Prism.Events;

namespace Bloom.Events
{
    /// <summary>
    /// This event is published with a user to change the active user to that user.
    /// </summary>
    /// <seealso cref="Bloom.State.Domain.Models.User" />
    public class ChangeUserEvent : PubSubEvent<User> { }

    /// <summary>
    /// This event is published to show a change user modal.
    /// </summary>
    public class ShowChangeUserModalEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published to show a user profile modal.
    /// </summary>
    public class ShowUserProfileModalEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published after the active user has been changed.
    /// </summary>
    public class UserChangedEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published when any properties of the active user have been updated.
    /// </summary>
    public class UserUpdatedEvent : PubSubEvent<object> { }
}
