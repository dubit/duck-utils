# MathUtils

## Consts

```public const float PI = Mathf.PI;```

```public const float PI_2 = Mathf.PI * 2.0f;```

```public const float PI_HALF = Mathf.PI * 0.5f;```

## Methods

### IsOdd
```c#
public static bool IsOdd(this int value)
```
#### Summary
Returns true if the target integer is an odd number. Usage is inteded as an extension method.

### IsEven
```c#
public static bool IsEven(this int value)
```
#### Summary 
Returns true if the target integer is an even number. Usage is inteded as an extension method.

### IsBetween
```c#
public static bool IsBetween(this int value, int a, int b, bool inclusive = false)
```
```c#
public static bool IsBetween(this float value, float a, float b, bool inclusive = false)
```
#### Summary
Returns true if the target number is in between two values. The check can be inclusive or exclusive. It defaults to exclusive. There are 2 overloads, one for `int` and one for `float` types.

### InverseLerpUnclamped
```c#
public static float InverseLerpUnclamped(float a, float b, float value)
```
#### Summary
Calculates the linear parameter t that produces the interpolant value within the range [a, b].

### NormalizeAngle360
```c#
public static float NormalizeAngle360(float angleInDegrees)
```
#### Summary
Returns a normalized angle that will always be between 0 and 360.

### NormalizeAngle180
```c#
public static float NormalizeAngle180(float angleInDegrees)
```
#### Summary
Returns a normalized angle that will always be between -180 and 180.

### GyroToUnityRotation
```c#
public static Quaternion GyroToUnityRotation(Quaternion attitude)
```
#### Summary
Converts Gyroscope Attitude into Unity Rotation
		