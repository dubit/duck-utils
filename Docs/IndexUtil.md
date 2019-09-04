# IndexUtil

Index util allows you to increment an integer value, that's used as an index, and will automatically clamp it for you and optionally wrap around in both directions.

## Example
```c#
characterSelectionIndex = characterSelectionIndex.Increment(characters.Count);
characterSelectionIndex = characterSelectionIndex.Decrement(characters.Count);
```

It wraps around by default if the index exceeds the range. To disable wrap around just specify the 2nd parameter.
If wrapAround is set to false it will just clamp it instead.

```c#
characterSelectionIndex = characterSelectionIndex.Increment(characters.Count, false);
characterSelectionIndex = characterSelectionIndex.Decrement(characters.Count, false);
```

## Methods

### Increment
```c#
public static int Increment(this int value, int max, bool wrapAround = true)
```
#### Summary
Increments the target index, and keeps it within the range of 0 (inclusive) to max (exclusive). If wrap around is set to true it will wrap, otherwise it will clamp

### Decrement
```c#
public static int Decrement(this int value, int max, bool wrapAround = true)
```

#### Summary
Decrements the target index, and keeps it within the range of 0 (inclusive) to max (exclusive). If wrap around is set to true it will wrap, otherwise it will clamp
