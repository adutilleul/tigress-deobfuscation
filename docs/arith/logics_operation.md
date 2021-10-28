# Logics Operations

## Equal

```c
  return (((unsigned int )((((a - b) ^ ((a - b) >> 31)) - ((a - b) >> 31)) - 1) >> 31U) & 1);
  return (((unsigned int )(~ ((a - b) | (b - a))) >> 31U) & 1);
  return (((unsigned int )((((a - b) + ((a - b) >> 31)) ^ ((a - b) >> 31)) - 1) >> 31U) & 1);
  return (((unsigned int )(((a - b) >> 31) - ((a - b) ^ ((a - b) >> 31))) >> 31U) & 1);
  return (((unsigned int )(((a - b) - (((a - b) + (a - b)) & ((a - b) >> 31))) - 1) >> 31U) & 1);
  return (((unsigned int )(((a - b) + (1 << 31)) - ((((a - b) + (1 << 31)) + ((a - b) + (1 << 31))) & (((a - b) + (1 << 31)) >> 31))) >> 31U) & 1);
  return (((unsigned int )((((a - b) + (1 << 31)) ^ (((a - b) + (1 << 31)) >> 31)) - (((a - b) + (1 << 31)) >> 31)) >> 31U) & 1);
  return (((unsigned int )(((a - b) - (((a - b) << 1) & ((a - b) >> 31))) - 1) >> 31U) & 1);
```

## Not Equal

```c
  return (((unsigned int )(((a - b) >> 31) - ((a - b) ^ ((a - b) >> 31))) >> 31U) & 1);
  return (((unsigned int )((a - b) | (b - a)) >> 31U) & 1);
```
## Lesser than 

```c
  return (((unsigned int )((a - b) ^ ((a ^ b) & ((a - b) ^ a))) >> 31U) & 1);
  return (((unsigned int )((a & ~ b) | (~ (a ^ b) & (a - b))) >> 31U) & 1);
  return (((unsigned int )((((b - a) & - (b >= a)) >> 31) - (((b - a) & - (b >= a)) ^ (((b - a) & - (b >= a)) >> 31))) >> 31U) & 1);

```
 
## Lesser of equal than

```c
  return (((unsigned int )((~ (a ^ b) >> 1) + (a & ~ b)) >> 31U) & 1);
  return (((unsigned int )((a | ~ b) & ((a ^ b) | ~ (b - a))) >> 31U) & 1);
```

## Greater than

```c
  return (((unsigned int )((b - a) ^ ((b ^ a) & ((b - a) ^ b))) >> 31U) & 1);
  return (((unsigned int )((b & ~ a) | (~ (b ^ a) & (b - a))) >> 31U) & 1);
  return (((unsigned int )(((((a - b) & - (a >= b)) + ((a - b) & - (a >= b))) & (((a - b) & - (a >= b)) >> 31)) - ((a - b) & - (a >= b))) >> 31U) & 1);
  return (((unsigned int )((b - a) ^ ((b ^ a) & ((b - a) ^ b))) >> 31U) & 1);
  
```

## Greater or equal than

```c
  return (((unsigned int )((b | ~ a) & ((b ^ a) | ~ (a - b))) >> 31U) & 1);
  return (((unsigned int )((~ (b ^ a) >> 1) + (b & ~ a)) >> 31U) & 1);
  return (((unsigned int )((~ (a ^ b) >> 1) + (a & ~ b)) >> 31U) & 1);
  return (((unsigned int )((b | ~ a) & ((b ^ a) | ~ (a - b))) >> 31U) & 1);

```


## And
```c
int logic_and(int a , int b ) 
{ 
  int tmp ;

  {
  if (a) {
    if (b) {
      tmp = 1;
    } else {
      tmp = 0;
    }
  } else {
    tmp = 0;
  }
  return (tmp);
}
}
```


```c
int logic_or(int a , int b ) 
{ 
  int tmp ;

  {
  if (a) {
    tmp = 1;
  } else
  if (b) {
    tmp = 1;
  } else {
    tmp = 0;
  }
  return (tmp);
}
}
```


