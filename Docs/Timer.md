# Timer

## Static Methods
Creates a timer that invokes the action after the specified duration. Returns the timer.
```c#
public static Timer SetTimeout(float duration, Action onComplete = null)
```

## Constructor
Creates a new timer instance without starting it.
```c#
public Timer(float duration = 0f)
```


## Instance Properties

| Type | Name       | get | set | Description |
|:---- |:-----------|:----|:----|:------------|
| int | LoopCount | yes | yes | Gets or sets the amount of loops left to run on the timer. -1 is infinite. |
| float | Duration | yes | yes | Gets or sets the Duration of the timer. (if this is less than or equal to zero the timer will finihs instantly) |
| bool | UseScaledTime | yes | yes | Gets or sets if the timer will run against `Time.timescale` or not. |
| float | TimeRemaining | yes | no | Gets the amount of time remaining on the timer |
| float | TimeElapsed | yes | no | Gets the amount of time elapsed on the timer |
| bool | IsLooping | yes | no | True if the timer is looping (if LoopCount != 0) |
| bool | IsRunning | yes | no | True if the timer is currently running |
| bool | IsPaused | yes | no | True if the timer is currently paused |

## Methods

## Start

There are several overloads for starting timers
All start overloads reset the timer if it's already running

Starts the timer. The onComplete will be called when the timer has completed.
```c#
public void Start(Action onComplete = null)
```
Starts the timer, and overrides the duration. The onComplete will be called when the timer has completed.
```c#
public void Start(float duration, Action onComplete = null)
```
Starts the timer with loops. Specify the loop count, an optional loop callback and complete callback.
```c#
public void Start(int loopCount, Action onLoopComplete = null, Action onComplete = null)
```
Starts the timer with loops and allows a new duration. (see the above 2 overloads)
```c#
public void Start(float duration, int loopCount, Action onLoopComplete = null, Action onComplete = null)
```
## Stop
Stops the timer. Will not invoke any callbacks
```c#
public void Stop()
```

## Pause
Pauses the timer
```c#
public void Pause()
```

## Resume
Resumes the timer
```c#
public void Resume();
```

## Reset
Resets the timers elapsed time, to the duration. The timer will not affect playback state. It will not pause, resume, stop or play.
It will not affect the amount of loops remaining.
```c#
public void Reset();
```