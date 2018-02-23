using System;
using Bloom.State.Domain.Models;
using Prism.Events;

namespace Bloom.PubSubEvents
{
    /// <summary>
    /// This event is published with library connection information to add it to the active connections.
    /// </summary>
    /// <seealso cref="Bloom.State.Domain.Models.LibraryConnection" />
    public class ConnectionAddedEvent : PubSubEvent<LibraryConnection> { }

    /// <summary>
    /// This event is published with a library identifier to remove it from the active connections.
    /// </summary>
    /// <seealso cref="Guid" />
    public class ConnectionRemovedEvent : PubSubEvent<Guid> { }

    /// <summary>
    /// This event is published with a user to change the active user to that user.
    /// </summary>
    /// <seealso cref="Bloom.State.Domain.Models.User" />
    public class ChangeUserEvent : PubSubEvent<User> { }

    /// <summary>
    /// This event is published after the active user has been changed.
    /// </summary>
    public class UserChangedEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published to save application and suite states.
    /// </summary>
    public class SaveStateEvent : PubSubEvent<ApplicationState> { }
}
