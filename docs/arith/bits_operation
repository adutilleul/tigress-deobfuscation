# Bits Operations

## And

```c
  return (((~ a | b) + a) + 1);
  return ((~ a | b) - ~ a);
```

## XOR

```c
  return ((a | b) - (a & b));
  return (((a - b) - ((a | ~ b) << 1)) - 2);
  return (((a - b) - ((a | ~ b) + (a | ~ b))) - 2);
```

## Or

```c
  return ((a & ~ b) + b);
  return (((a + b) + 1) + ((- a - 1) | (- b - 1))); 
```

## LShift
No changes.
## RShift 
No changes.

## Neg

```c
  return (- a - 1);
```

return ((a & b) * (a | b) + (a & ~ b) * (~ a & b));
           1         15   +  48  
           7 * 9