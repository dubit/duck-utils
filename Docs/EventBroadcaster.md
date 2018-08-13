# EventBroadcaster

The EventBroadcaster is a way of sending events or messages to objects, that have a component or components implementing an interface.

It provides a way to listen for events without actually subscribing, removing the need to unsubscribe, and maintaining a collection of callbacks.

It's a pattern used by unity's UI messages such as IPointerEnterHandler etc [See this link for more info](https://docs.unity3d.com/Manual/SupportedEvents.html)

# Example
A common example is you want to respond to a specific event. Let's say the event of a player collecting a pickup.
You have detected the collision, and you want multiple things to happen:
* Destroy the pickup
* Play some audio
* Show an animation on the pickup as it's gona away (eg: particles)
* Show an animation on the player

There are many approaches to this problem. It could all be dealt with in the same class. It could be dealt with by implementing an event on the player/pickup and several different components listen to it. In this situation, they have a dependency on the player or pickup class in order to actually subscribe to it.
Using the event broadcaster it would look something like this

1. Define an interface that you implement in order to be notified

```c#
public interface IPickupCollectedHandler
{
    void HandlePickupCollected();
}
```

2. When the collision happened use the EventBroadcaster
```c#
private void OnCollisionEnter(Collider collider)
{
    // tell both the other object and this object
    collider.BroadcastEvent<IPickupCollectedHandler>(
        t => t.HandlePickupCollected();
    );
    gameObject.BroadcastEvent<IPickupCollectedHandler>(
        t => t.HandlePickupCollected();
    );
}
```

3. Now both objects are setup to receive the event. Just implement the interface as MonoBehaviours and attach those handler components to the objects.
Examples:
```c#
class PickupSoundFX : MonoBehaviour, IPickupCollectedHandler
class PickupVisualFX : MonoBehaviour, IPickupCollectedHandler
```

The EventBroadcaster also broadcasts to children by default. This can be disabled by including the optional parameter.

## API

### BroadcastEvent
```c#
public static void BroadcastEvent<T>(this GameObject gameObject, Action<T> execute, bool broadcastToChildren = true)
```
```c#
public static void BroadcastEvent<T>(this Component component, Action<T> execute, bool broadcastToChildren = true)
```
### Summary
Looks for components of type `T` on the target object and in children (if `broadcastToChildren` is set to true). For all components found, the given action will be executed.