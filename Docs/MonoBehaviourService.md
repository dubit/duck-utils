# MonoBehaviourService

The purpose of the mono behaviour service is to provide an interface to register callbacks to the mono behaviour messages from non monobehaviuor classes. The benefit of this is not needing to implement and create an instance of a MonoBehaviour, which can (depending on situation) be sub optimal, bad for architecture, or require management of multiple scene objects which may have no reason to be in the scene. 
It allows you to register for updates or application events. It can also run coroutines for you 

It is implemented as a singleton, accessible from anywhere. When accessed, a single hidden object is placed into the scene to capture the events and run the coroutines is created. It is marked as `DoNotDestroyOnLoad()`.

## Methods

### StartCoroutine
Starts a coroutine function, and returns it.
```c#
public Coroutine StartCoroutine(IEnumerator toRun)
```

Example:
```c#
MonoBehaviourService.Instance.StartCoroutine(MyCoroutine());
```

### StopCoroutine
Stops the specified coroutine
```c#
public void StopCoroutine(Coroutine toStop)
```


## Events

Most events are just hooks into the unity messages described [here](https://docs.unity3d.com/Manual/ExecutionOrder.html).

| Name | Parameter(s) | Equivelant | Description |
|:-----|:------------ |:-----------|:------------|
| OnUpdate | none | `Update` | Called once per frame. |
| OnLateUpdate | none | `LateUpdate` | Called once per frame at the end of the frame. |
| OnFixedUpdate | none | `FixedUpdate` | Called at fixed intervals |
| ApplicationPause | `bool`| `OnApplicationPause` | Called when application is paused or unpaused |
| ApplicationQuit | none | `OnApplicationQuit` | Called when the application exits |
| ApplicationFocus | `bool` | `OnApplicationFocus` | Called when the application gains or loses focus |
| OnLevelLoaded | none | `SceneManager.sceneLoaded` | Called after a scene has completed loading|
