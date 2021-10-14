
### Addition

```C
/* Different return values of a+b */
/* References for Hacker's Delight p28 */

((a - ~ b) - 1); // Depth 1 : Ref f)

((a ^ b) + ((a & b) + (a & b))); // 2(a&b) + (a^b) : Ref g)
((a ^ b) + ((a & b) << 1));

(((a | b) + (a | b)) - (a ^ b)); // 2(a|b) - a^b : Ref i)
(((a | b) << 1) - (a ^ b));

(((a ^ ~ b) + ((a | b) + (a | b))) + 1); // (a^~b) + 2(a|b) + 1
(((a ^ ~ b) + ((a | b) << 1)) + 1);

```

### Soustraction

```C
((a + ~ b) + 1); // Ref j)

((a & ~ b) - (~ a & b)); // Ref l)

(((a & ~ b) << 1) - (a ^ b)); // Ref m)
(((a & ~ b) + (a & ~ b)) - (a ^ b));

((a ^ b) - ((~ a & b) << 1)); // Ref k)

```

### Multiplication

```C
((a & b) * (a | b) + (a & ~ b) * (~ a & b));


```

### Division

```C
(a / b);
```

### Incrémentation
#### a++
```C
int tmp ;

{
tmp = a;
a ++;
return (tmp);
}


int tmp ;

{
tmp = a;
a = (a - ~ 1) - 1;
return (tmp);
}
```

#### ++a

```C
{
a ++;
return (a);
}
```

Même techniques pour la décrémentation

### Modulo

Inchangé


### assignations

Source :
```C
a += b;
a -= b;
a *= b;
a /= b;
a %= b;
a &= b;
a |= b;
a ^= b;
a <<= b;
a >>= b;
```

Obfusqué :
```C
a = ((a ^ ~ b) + ((a | b) << 1)) + 1;
a = ((a & ~ b) + (a & ~ b)) - (a ^ b);
a = (a & b) * (a | b) + (a & ~ b) * (~ a & b);
a /= b;
a %= b;
a = (~ a | b) - ~ a;
a = ((a + b) + 1) + ((- a - 1) | (- b - 1));
a = ((a - b) - ((a | ~ b) + (a | ~ b))) - 2;
a <<= b;
a >>= b;
```

Et autres transformations, cf les opérations précédentes.

`a += b; -> a = a + b`

###
