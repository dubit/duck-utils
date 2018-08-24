# TransformSnapshot

TransformSnapshot is used for taking a snapshot of a transform and and applying to another transform, or reverting the original to the snapshot state.
It takes a snapshot of all the basic transform properties like position, rotation, scale and the parent.

## Examples
To create a transform snapshot
```c#
var snapshot = new TransformSnapshot(transform);
```

To apply it to another transform
```c#
snapshot.ApplyTo(otherTransform)
```

To revert the original transform back to the snapshot state
```c#
snapshot.Apply();
```

